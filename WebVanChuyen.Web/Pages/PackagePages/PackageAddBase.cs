using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Linq;
using WebVanChuyen.Models.Models;
using WebVanChuyen.Web.Services;
using WebVanChuyen.Web.Services.Data;

namespace WebVanChuyen.Web.Pages.PackagePages
{
    public class PackageAddBase : ComponentBase
    {
        // Load Services
        [Inject] private IWarehouseService WarehouseService { get; set; }
        [Inject] private IEmployeeService EmployeeService { get; set; }
        [Inject] private IPackageService PackageService { get; set; }
        [Inject] private IRoutePathService RoutePathService { get; set; }
        [Inject] private IMapper Map { get; set; }
        [Inject] private NavigationManager Nav { get; set; }
        [Inject] private PackageStateContainer PackageStateContainer { get; set; }

        // Authentication infomation
        [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int UserPosition { get; set; }
        public string UserWarehouseId { get; set; }
        public Warehouse UserWarehouse { get; set; }
       
        // Warehouse
        protected bool ReloadDistricts { get; set; } = false;
        protected bool ReloadCommunes { get; set; } = false;
        protected string DestWarehouse { get; set; }
        protected List<Province> Provinces { get; set; }
        protected List<District> Districts { get; set; } = new List<District>();
        protected List<Commune> Communes { get; set; } = new List<Commune>();
        protected List<Warehouse> Warehouses { get; set; }

        // Employee
        protected Employee Receptionists { get; set; }

        // Package
        public int InProviceCost { get; } = 20000;
        protected Package Package { get; set; } = new Package();
        protected AddPackage AddPackage { get; set; } = new AddPackage();
        public PackageView PackageView { get; set; } = new PackageView();
        protected List<PackageDetails> Details { get; set; }
        protected List<PackageWeight> Weights { get; set; }

        // RoutePath
        public WHRoute? Route { get; set; }
        protected List<WHRoute> Routes { get; set; }

        // Other Variable
        public string ErrorMessage { get; set; } = string.Empty;


        protected override async Task OnInitializedAsync()
        {
            // Get user information
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            UserId = int.Parse(user.FindFirst("UserId").Value);
            UserName = user.FindFirst("UserName").Value;
            UserPosition = int.Parse(user.FindFirst("UserPosition").Value);
            UserWarehouseId = user.FindFirst("WorkWarehouse").Value;

            // Load relate to Warehouse
            Provinces = await WarehouseService.GetProvinces();
            Warehouses = await WarehouseService.GetWarehouses();
            UserWarehouse = Warehouses.First(wh => wh.WarehouseId == UserWarehouseId);

            // Load relate to Employee
            Receptionists = await EmployeeService.GetEmployee(UserId);

            // Load relate to Package
            Weights = (await PackageService.GetPackageWeights()).ToList();
            Routes = (await RoutePathService.GetRoutes()).ToList();

            // Set value
            AddPackage.ReceptionistId = UserId;
            AddPackage.LocaId = UserWarehouseId;

            // Set other value
            UserWarehouse = Warehouses.First(wh => wh.WarehouseId == UserWarehouseId);

            // Set state for transfer data
            PackageStateContainer.OnStateChange += StateHasChanged;
        }

        protected async void HandleValidSubmit()
        {
            // Check route of package
            if (AddPackage.RouteId != 0)
            {
                // Create package depend on destination
                Map.Map(AddPackage, Package);
                if (AddPackage.RouteId == null)
                {
                    Package.Status = (int)PackageStatus.WaitingDelivery;
                    Package = await PackageService.CreatePackage(Package);
                }
                else
                {
                    Package = await PackageService.CreatePackage(Package);
                }

                // Add register log
                PackageLog registerLog = new()
                {
                    PackageId = Package.PackageId,
                    LogNote = "Kien hang da duoc ky gui tai " + UserWarehouse.WarehouseName
                };
                await PackageService.AddPackageLog(registerLog, true);

                // Add delivery log if transport inside province
                /*
                if (AddPackage.RouteId == null)
                {
                    PackageLog deliveryLog = new PackageLog
                    {
                        PackageId = Package.PackageId,
                        LogNote = "Kien hang dang duoc giao, vui long chu y dien thoai"
                    };
                    await PackageService.AddPackageLog(deliveryLog, true);
                }
                */
                
                // Navigater to result page
                var provinceName = Provinces.First(p => p.ProvinceId == AddPackage.ProvinceId).ProvinceName;
                var districtName = Districts.First(p => p.DistrictId == AddPackage.DistrictId).DistrictName;
                var communeName = Communes.First(p => p.CommuneId == AddPackage.CommuneId).CommuneName;

                PackageStateContainer.SetValue(
                    Package, UserName, UserWarehouse.WarehouseName,
                    provinceName,districtName, communeName );
                Nav.NavigateTo("/package-view");
            }
            else
            {
                ErrorMessage = "Không tìm được tuyến đường cho kiện hàng";
            }
        }

        // Reset district if change province
        protected async void OnChangeProvince()
        {
            AddPackage.DistrictId = 0;
            AddPackage.CommuneId = 0;

            Districts.Clear();
            Communes.Clear();

            if(AddPackage.ProvinceId != "")
            {
                Districts = await WarehouseService.GetDistrictsByProvince(AddPackage.ProvinceId);

                SearchRoute();
                CalculationShipCost();
            }

            StateHasChanged();
        }

        protected async void OnChangeDistrict()
        {
            if(AddPackage.DistrictId != 0)
            {
                Communes.Clear();
                AddPackage.CommuneId = 0;

                Communes = await WarehouseService.GetCommunesByDistrict(AddPackage.DistrictId);

                StateHasChanged();
            }
        }

        // Search Route and related attributes
        protected async void SearchRoute()
        {
            // Check if transport is outside province
            if(AddPackage.ProvinceId != UserWarehouse.ProvinceId)
            {
                DestWarehouse = Warehouses.First(wh => wh.ProvinceId == AddPackage.ProvinceId).WarehouseId;
                Route = Routes.FirstOrDefault(route => route.StartPoint == UserWarehouseId && route.EndPoint == DestWarehouse ||
                route.StartPoint == DestWarehouse && route.EndPoint == UserWarehouseId);

                if (Route == null)
                {
                    StartAndEnd snE = new StartAndEnd() { StartPoint = UserWarehouseId, EndPoint = DestWarehouse };
                    Route = await RoutePathService.CreateRoute(snE);
                    // Force reload component
                    //NavigationManager.NavigateTo("/package/add", true);
                }

                AddPackage.RouteId = Route.RouteId;
            }
            else AddPackage.RouteId = null;
        }

        protected async void CalculationShipCost()
        {
            // Get additions weight cost
            int weightCost = 0;
            if (AddPackage.Weight >= 0 || AddPackage.Weight > 0.5)
            {
                // Order list by close to PackgeWeight
                var weightList = Weights.OrderBy(r => Math.Abs(AddPackage.Weight - r.Weight)).ToList();

                // Case 1: when Weight > baseWeight
                if (AddPackage.Weight > weightList.First().Weight)
                {
                    weightCost = weightList.Take(2).ToList().Last().WCost;
                }
                // Case 2: when Weight <= baseWeight
                else
                {
                    weightCost = weightList.First().WCost;
                }
            }

            // Check tranpost destination
            if (AddPackage.ProvinceId == UserWarehouse.ProvinceId) AddPackage.ShipCost = InProviceCost + weightCost;
            else
            {
                var numOfStop = Route.StopPointsList.Split(",").Length;
                AddPackage.ShipCost = (numOfStop - 1) * weightCost + Route.TotalShipCost;
            }
        }

        protected void OnChangeWeight()
        {
            CalculationShipCost();
            StateHasChanged();
        }

        protected void SetDefaultValue()
        {
            AddPackage.Weight = 1;
            AddPackage.SenderName = "Nguoi gui 1";
            AddPackage.SenderPhone = "09098809";
            AddPackage.SenderAddr = "123 d/c";
            AddPackage.ReceiverName = "Nguoi nhan 2";
            AddPackage.ReceiverPhone = "08086608";
            AddPackage.ReceiverAddr = "456 d/c";
        }

        public void Dispose()
        {
            PackageStateContainer.OnStateChange -= StateHasChanged;
        }
    }
}
