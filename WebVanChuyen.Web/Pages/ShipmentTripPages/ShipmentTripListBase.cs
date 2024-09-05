using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using WebVanChuyen.Models.Models;
using WebVanChuyen.Web.Services;

namespace WebVanChuyen.Web.Pages.ShipmentTripPages
{
    public class ShipmentTripListBase : ComponentBase
    {
        [Inject] private IWarehouseService WarehouseService { get; set; }
        [Inject] private IEmployeeService EmployeeService { get; set; }
        [Inject] private IShipmentTripService ShipmentTripService { get; set; }
        [Inject] private IPackageService PackageService { get; set; }

        // Authentication infomation
        [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        protected ClaimsPrincipal User { get; set; }
        protected int EmployeeId { get; set; }
        protected string WorkWarehouse { get; set; }

        // Warehouse
        protected List<Warehouse> Warehouses { get; set; }

        // Employee
        protected List<Employee> Drivers { get; set; }

        // ShipmentTrip
        protected List<ShipmentTrip> ShipmentTrips { get; set; } = new List<ShipmentTrip>();
        protected List<ShipmentTrip> ViewShipmentTrips { get; set; } = new List<ShipmentTrip>();

        // Other Variable
        protected bool IsStart { get; set; } = true;
        protected string ErrorMessage { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            // Get user information
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            User = authState.User;
            EmployeeId = int.Parse(User.FindFirst("UserId").Value);
            WorkWarehouse = User.FindFirst("WorkWarehouse").Value;

            // Load data
            Warehouses = await WarehouseService.GetWarehouses();
            Drivers = (await EmployeeService.GetDrivers()).ToList();

            // Load shipment trip list depend on user role
            await LoadShipmentTrips();
        }

        // Get case of shipment trip
        public int GetButtonType(int statusIndex)
        {
            if (User.IsInRole("Manager"))
            {
                // From warehouse
                // Case 1: Add package and start shipment trip
                if (IsStart && statusIndex == (int)ShipmentTripStatus.Pending) return 1;
                // Case 2: Shipment trip pending for confirm 
                if (IsStart && statusIndex == (int)ShipmentTripStatus.Arrived) return 2;

                // Case 3: Shipment trip is transport
                if (statusIndex == (int)ShipmentTripStatus.Transporting) return 3;

                // Comming to warehouse
                // Case 4: Shipment is waiting in the start
                if (!IsStart && statusIndex == (int)ShipmentTripStatus.Pending) return 4;
                // Case 5: Confirm request complete shipment trip
                if (!IsStart && statusIndex == (int)ShipmentTripStatus.Arrived) return 5;
            }
            else if (User.IsInRole("Staff"))
            {
                // Case 6: Staff view packages in shipment trip
                if (statusIndex == (int)ShipmentTripStatus.Pending) return 6;
            }
            else
            {
                // Case 7: Driver await shipment to start
                if (statusIndex == (int)ShipmentTripStatus.Pending) return 7;
                // Case 8: Request confirm arrived shipment trip
                if (statusIndex == (int)ShipmentTripStatus.Transporting) return 8;
                // Case 9: Wait for confirm arrived
                if (statusIndex == (int)ShipmentTripStatus.Arrived) return 9;
            }

            return 0;
        }

        // Call when any shipment trip change status
        public async Task LoadShipmentTrips()
        {
            if (!User.IsInRole("Driver"))
            {
                ShipmentTrips = await ShipmentTripService.GetShipmentTrips(WorkWarehouse);

                if (User.IsInRole("Manager"))
                {
                    // Get depend list shipment trips incoming or not start
                    if (IsStart) ViewShipmentTrips = ShipmentTrips.Where(s => s.StartWH == WorkWarehouse).ToList();
                    else ViewShipmentTrips = ShipmentTrips.Where(s => s.EndWH == WorkWarehouse).ToList();
                }
                else if (User.IsInRole("Staff"))
                {
                    // Get list shipment trips is not start
                    ViewShipmentTrips = ShipmentTrips.Where(s => s.StartWH == WorkWarehouse).ToList();
                }
            }
            else if(User.IsInRole("Driver"))
            {
                // Get list shipment trips is assign to driver
                ShipmentTrips = await ShipmentTripService.GetShipmentTripsByDriver(EmployeeId);
                ViewShipmentTrips = ShipmentTrips.Where(s => s.Status != (int)ShipmentTripStatus.Complete)
                    .OrderByDescending(shipmentTrip => shipmentTrip.ShipmentTripId).ToList();
            }
        }

        // Filter shipment trip list depend is from or coming
        public async Task CheckIsStart()
        {
            await LoadShipmentTrips();

            StateHasChanged();
        }

        public async void ShipmentTripTransporting(int shipmentTripId)
        {
            var packages = await PackageService.GetPackageLogsByShipmentTrip(shipmentTripId);
            if (packages.Any())
            {
                UpdateShipmentTripStatus shipmentTrip = new UpdateShipmentTripStatus
                {
                    ShipmentTripId = shipmentTripId,
                    Status = (int)ShipmentTripStatus.Transporting
                };

                await ShipmentTripService.UpdateShipmentTrip(shipmentTrip);
                await LoadShipmentTrips();

                StateHasChanged();
            }
            else
            {
                ErrorMessage = "Không có kiện hàng nào trong chuyến xe!";
            }
        }

        public async void ShipmentTripArrived(int shipmentTripId)
        {
            UpdateShipmentTripStatus shipmentTrip = new UpdateShipmentTripStatus
            {
                ShipmentTripId = shipmentTripId,
                Status = (int)ShipmentTripStatus.Arrived
            };
            await ShipmentTripService.UpdateShipmentTrip(shipmentTrip);
            await LoadShipmentTrips();

            StateHasChanged();
        }

        public async void ShipmentTripComplete(int shipmentTripId)
        {
            UpdateShipmentTripStatus shipmentTrip = new UpdateShipmentTripStatus
            {
                ShipmentTripId = shipmentTripId,
                Status = (int)ShipmentTripStatus.Complete
            };

            await ShipmentTripService.UpdateShipmentTrip(shipmentTrip);
            await LoadShipmentTrips();

            StateHasChanged();
        }
    }
}
