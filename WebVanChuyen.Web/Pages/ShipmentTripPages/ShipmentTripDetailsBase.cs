using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using WebVanChuyen.Models.Models;
using WebVanChuyen.Web.Pages.RoutePathPages;
using WebVanChuyen.Web.Services;

namespace WebVanChuyen.Web.Pages.ShipmentTripPages
{
    public class ShipmentTripDetailsBase : ComponentBase
    {
        [Inject] private IWarehouseService WarehouseService { get; set; }
        [Inject] private IShipmentTripService ShipmentTripService { get; set; }
        [Inject] private IPackageService PackageService { get; set; }
        [Inject] private IRoutePathService RoutePathService { get; set; }
        [Inject] private NavigationManager Nav { get; set; }

        // Authentication infomation
        [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        // Shipment
        protected ShipmentTrip ShipmentTrip { get; set; } = new ShipmentTrip();

        // Packages
        protected List<int> SelectPackages { get; set; } = new List<int>();
        protected List<Package> PackagesCanTranport { get; set; } = new List<Package>();
        protected List<PackageLog> PackageLogs { get; set; } = new List<PackageLog>();

        // Route
        protected List<WHRoute> Routes { get; set; } = new List<WHRoute>();

        // Other variable
        [Parameter] public string Id { get; set; }
        protected bool EditValid { get; set; } = true;
        protected string DestWarehouseName { get; set; }
        protected bool LoadingPage { get; set; } = false;
        protected bool IsManager { get; set; } = false;


        protected async override Task OnInitializedAsync()
        {
            if(Id == null || int.Parse(Id) == 0) Nav.NavigateTo("/shipment-trip/list");
            else
            {
                // Load user
                var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;
                if (user.IsInRole("Manager")) IsManager = true;

                // Load relate to Shipment
                ShipmentTrip = await ShipmentTripService.GetShipmentTrip(int.Parse(Id));
                if (ShipmentTrip.Status != (int)ShipmentTripStatus.Pending) EditValid = false;

                // Load relate to Route
                Routes = await RoutePathService.GetRoutes();

                // Load relate to Package
                await LoadPackages();

                // Load other 
                DestWarehouseName = (await WarehouseService.GetWarehouse(ShipmentTrip.EndWH)).WarehouseName;
            }
        }

        // Load packages in trip and packages can add into
        protected async Task LoadPackages()
        {
            PackageLogs.Clear();
            PackagesCanTranport.Clear();
            SelectPackages.Clear();

            // Load packages in shipment by search package log create when add to shipment trip
            PackageLogs = await PackageService.GetPackageLogsByShipmentTrip(ShipmentTrip.ShipmentTripId);

            // Load packages can add to shipment trip
            var packages = await PackageService.GetPackagesCanTransport(ShipmentTrip.StartWH);
            foreach (var pack in packages)
            {
                // Get list of middle warehouse in route
                var stopString = Routes.FirstOrDefault(route => route.RouteId == pack.RouteId).StopPointsList;

                var numStart = stopString.IndexOf(ShipmentTrip.StartWH);
                var numEnd = stopString.IndexOf(ShipmentTrip.EndWH);

                // Add package to list
                if (numStart < numEnd) PackagesCanTranport.Add(pack);
            }
        }

        // Remove package already in shipment trip
        protected async Task RemovePackage(int packageLogId)
        {
            LoadingPage = true;
            await Task.Delay(2000);

            await PackageService.DeletePackageLog(packageLogId);
            await LoadPackages();
            
            LoadingPage = false;

            StateHasChanged();
        }

        // Edit list of select packageId can add into shipment trip
        protected void EditSelectPackages(int packageId)
        {
            if (SelectPackages.Contains(packageId)) SelectPackages.Remove(packageId);
            else SelectPackages.Add(packageId);
        }

        // Add select packages into shipment trip
        protected async Task AddPackgesToShipmentTrip()
        {
            LoadingPage = true;
            await Task.Delay(2000);

            //List<PackageLog> PackageLogs = new();
            foreach (var package in SelectPackages)
            {
                PackageLog newPackageLog = new()
                {
                    PackageId = package,
                    ShipmentTripId = ShipmentTrip.ShipmentTripId,
                    TruckId = ShipmentTrip.TruckId,
                    LogNote = "Dang duoc van chuyen den " + DestWarehouseName
                };

                await PackageService.AddPackageLog(newPackageLog, false);
            }

            await LoadPackages();
            LoadingPage = false;

            StateHasChanged();
        }
    }
}
