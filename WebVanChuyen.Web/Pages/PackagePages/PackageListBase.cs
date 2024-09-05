using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Security.Claims;
using WebVanChuyen.Models.Models;
using WebVanChuyen.Web.Services;
using WebVanChuyen.Web.Shared;

namespace WebVanChuyen.Web.Pages.PackagePages
{
    public class PackageListBase : ComponentBase
    {
        [Inject] private IWarehouseService WarehouseService { get; set; }
        [Inject] private IEmployeeService EmployeeService { get; set; }
        [Inject] private IPackageService PackageService { get; set; }

        // Authentication infomation
        [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        protected ClaimsPrincipal User { get; set; }
        protected int EmployeeId { get; set; }
        protected string WorkWarehouse { get; set; }

        // Warehouse
        protected List<Warehouse> Warehouses { get; set; } = new List<Warehouse>();

        // Package
        protected List<Package> PackagesList { get; set; } = new List<Package>();

        // Other variable
        protected bool IsStaff {  get; set; }
        protected bool IsRegister { get; set; } = true;
        protected string ReceiverId { get; set; }
        protected ConfirmDelivery Confirmation { get; set; }


        protected override async Task OnInitializedAsync()
        {
            // Get user information
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            User = authState.User;
            EmployeeId = int.Parse(User.FindFirst("UserId").Value);
            WorkWarehouse = User.FindFirst("WorkWarehouse").Value;

            // Define how to load data
            if(User.IsInRole("Staff")) IsStaff = true;
            else IsStaff = false;

            // Warehouse relate
            Warehouses = await WarehouseService.GetWarehouses();

            // Package relate
            await LoadPackages();

        }

        protected async Task LoadPackages()
        {
            // Load data for staff role else load data for shipper
            if (IsStaff)
            {
                PackagesList = await PackageService.GetPackages(WorkWarehouse, true);
            }
            else
            {
                if (IsRegister) PackagesList = await PackageService.GetPackagesCanDelivery(WorkWarehouse);
                else PackagesList = await PackageService.GetPackagesByShipper(EmployeeId);
            }
        }

        protected string PackageCost(Package package)
        {
            int value;
            // Unpaid ship cost
            if (package.PayStatus == (int)PaymentStatus.Unpaid) value = package.Collect + package.ShipCost;
            else value = package.Collect;

            string result = string.Format("{0:N0}", value);

            return result;
        }

        protected string LocationName(Package package)
        {
            var name = Warehouses.First(wh => wh.WarehouseId == package.LocaId).WarehouseName;
            if (name != null) return name;
            else return "N/a";
           
        }

        protected int GetButtonType(Package package)
        {
            if (User.IsInRole("Shipper"))
            {
                // Case 1: Show register package 
                if (package.ShipperId == null) return 1;
                // Case 1: Show confirm delivery 
                else return 2;
            }

            return 0;
        }

        protected async void RegisterPackage(int packageId)
        {
            PackageRegister register = new()
            {
                ShipperId = EmployeeId,
                PackageId = packageId
            };

            await PackageService.PackagShipperRegister(register);
            await LoadPackages();

            StateHasChanged();
        }

        // Show pop-up confirm delivery
        protected void ShowConfirmDelivery(int packageId)
        {
            Confirmation.Show(packageId);
        }

        // Event call-back form pop-up that run confirm delivery
        protected async void ConfirmDelivery(DeliveryConfirm deliveryConfirm)
        {
            if (deliveryConfirm.IsConfirm)
            {
                PackageDelivery confirm = new()
                {
                    PackageId = deliveryConfirm.PackageId,
                    ReceiverId = deliveryConfirm.ReceiverId,
                };

                await PackageService.PackageConfirmDelivery(confirm);
                await LoadPackages();

                StateHasChanged();
            }
        }
    }
}
