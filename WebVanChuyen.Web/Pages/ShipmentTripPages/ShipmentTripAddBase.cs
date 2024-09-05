using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using WebVanChuyen.Models.Models;
using WebVanChuyen.Web.Services;
using WebVanChuyen.Web.Services.Data;

namespace WebVanChuyen.Web.Pages.ShipmentTripPages
{
    public class ShipmentTripAddBase : ComponentBase
    {
        [Inject] private IWarehouseService WarehouseService { get; set; }
        [Inject] private IEmployeeService EmployeeService { get; set; }
        [Inject] private IRoutePathService RoutePathService { get; set; }
        [Inject] private IShipmentTripService ShipmentTripService { get; set; }
        [Inject] private IPackageService PackageService { get; set; }
        [Inject] private IMapper Map { get; set; }
        [Inject] private NavigationManager Nav { get; set; }
        [Inject] private ShipmentTripStateContainer ShipmentTripStateContainer { get; set; }

        // Authentication infomation
        [Inject] AuthenticationStateProvider authenticationStateProvider { get; set; }
        private ClaimsPrincipal User { get; set; }
        protected string WorkWarehouse { get; set; }

        // Warehouse
        protected List<Province> Provinces { get; set; } = new List<Province>();
        protected List<Warehouse> Warehouses { get; set; }

        // Employee
        protected List<Employee> Employees { get; set; } = new List<Employee>();
        protected List<Employee> Driver1 { get; set; } = new List<Employee>();
        protected List<Employee> Driver2 { get; set; } = new List<Employee>();

        // Route
        protected List<WHRoute> Routes { get; set; } = new List<WHRoute>();

        // Package
        protected List<Package> Packages { get; set; } = new List<Package>();
        protected List<Package> TransportPackages { get; set; } = new List<Package>();
        protected List<int> SelectPackages { get; set; } = new List<int>();

        // PackageLog
        protected List<PackageLog> PackageLogs { get; set; } = new List<PackageLog>();

        // ShipmentTrip
        protected ShipmentTrip ShipmentTrip { get; set; } = new ShipmentTrip();
        protected AddShipmentTrip AddShipmentTrip { get; set; } = new AddShipmentTrip();

        // Truck
        protected List<Truck> Trucks { get; set; } = new List<Truck>();

        // Other variable
        protected string WorkWarehouseName { get; set; }
        protected string ErrorMessage { get; set; }
        protected string DestWarehouseName { get; set; }
        

        protected override async Task OnInitializedAsync()
        {
            // Get user information
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            User = authState.User;
            //UserPosition = user.FindFirst("UserPosition").Value;  
            WorkWarehouse = User.FindFirst("WorkWarehouse").Value;

            // Load relate to Warehouse
            Warehouses = await WarehouseService.GetAdjacentWarehouses(WorkWarehouse);
            Provinces = await WarehouseService.GetProvinces(WorkWarehouse);

            // Load relate to Employee
            Employees = await EmployeeService.GetDriverByLocation(WorkWarehouse);
            Driver1 = Employees;
            Driver2 = Employees;

            // Load relate to Route
            Routes = await RoutePathService.GetRoutes();

            // Load relate to Package
            Packages = await PackageService.GetPackagesCanTransport(WorkWarehouse);

            // Load Truck
            Trucks = await ShipmentTripService.GetTrucksByWarehouse(WorkWarehouse);

            // Set default value
            AddShipmentTrip.Manager = int.Parse(User.FindFirst("UserId").Value);
            AddShipmentTrip.StartWH = WorkWarehouse;
            AddShipmentTrip.BeginT = DateTime.Today.AddHours(+1);

            // Set View value
            WorkWarehouseName = (await WarehouseService.GetWarehouse(WorkWarehouse)).WarehouseName;
        }

        protected async Task HandleValidSubmit()
        {
            // Check shipment trip have packages assign
            if (SelectPackages.Count > 0)
            {
                // Set null for driver 2
                if (AddShipmentTrip.Driver2 == 0) AddShipmentTrip.Driver2 = null;

                Map.Map(AddShipmentTrip, ShipmentTrip);
                var newShipmentTrip = await ShipmentTripService.CreateShipmentTrip(ShipmentTrip);

                List<PackageLog> PackageLogs = new();
                foreach (var package in SelectPackages)
                {
                    PackageLog newPackageLog = new()
                    {
                        PackageId = package,
                        ShipmentTripId = newShipmentTrip.ShipmentTripId,
                        TruckId = AddShipmentTrip.TruckId,
                        LogNote = "Dang duoc van chuyen den " + DestWarehouseName
                    };

                    var result = await PackageService.AddPackageLog(newPackageLog, false);
                    PackageLogs.Add(result);
                }

                string Driver2Name;
                if (AddShipmentTrip.Driver2 != null) Driver2Name = Employees.First(emp => emp.EmployeeId == AddShipmentTrip.Driver2).EmployeeName;
                else Driver2Name = "";

                ShipmentTripInfo ShipmentTripInfo = new()
                {
                    ShipmentId = newShipmentTrip.ShipmentTripId,
                    TruckId = AddShipmentTrip.TruckId,
                    Driver1Name = Employees.First(emp => emp.EmployeeId == AddShipmentTrip.Driver1).EmployeeName,
                    Driver2Name = Driver2Name,
                    BeginT = AddShipmentTrip.BeginT,
                    StartWHName = WorkWarehouseName,
                    EndWHName = Warehouses.First(wh => wh.WarehouseId == AddShipmentTrip.EndWH).WarehouseName,
                };
                ShipmentTripStateContainer.SetValue(ShipmentTripInfo, PackageLogs);
                Nav.NavigateTo("/shipment-trip-view", true);
            }
            else
            {
                ErrorMessage = "Xin chọn tối thiểu một kiện hàng";
            }
        }

        // Reset value for District and load list of District
        protected void OnChangeProvince()
        {
            // Load package need to transport
            if (AddShipmentTrip.ProvinceId != "")
            {
                // Set destination for shipment trip
                var warehouse = Warehouses.First(wh => wh.ProvinceId == AddShipmentTrip.ProvinceId);
                AddShipmentTrip.EndWH = warehouse.WarehouseId;
                DestWarehouseName = warehouse.WarehouseName;

                // Load package transport to province
                LoadTransportPackages(AddShipmentTrip.EndWH);

                if (TransportPackages.Count == 0) ErrorMessage = "Không tìm được kiện hàng nào cần vận chuyển!";
            }

                StateHasChanged();
        }
        
        protected void OnChangedDriver1()
        {
            Driver2 = Employees.Where(driver => driver.EmployeeId != AddShipmentTrip.Driver1).ToList();
        }

        protected void OnChangedDriver2()
        {
            Driver1 = Employees.Where(driver => driver.EmployeeId != AddShipmentTrip.Driver2).ToList();
        }

        // Load package isvalid for transport to destination
        protected async void LoadTransportPackages(string destination)
        {
            // Empty package list
            TransportPackages.Clear();

            if (destination != null)
            {
                foreach (var package in Packages)
                {
                    // Get list of middle warehouse in route
                    var stopString = Routes.FirstOrDefault(route => route.RouteId == package.RouteId).StopPointsList;

                    var numStart = stopString.IndexOf(WorkWarehouse);
                    var numEnd = stopString.IndexOf(destination);

                    // Add package to list
                    if (numStart < numEnd) TransportPackages.Add(package);
                }
            }
        }

        protected void EditSelectPackages(int packageId)
        {
            if (SelectPackages.Contains(packageId))
            {
                SelectPackages.Remove(packageId);
            }
            else SelectPackages.Add(packageId);
        }

        public void Dispose()
        {
            ShipmentTripStateContainer.OnStateChange -= StateHasChanged;
        }
    }
}
