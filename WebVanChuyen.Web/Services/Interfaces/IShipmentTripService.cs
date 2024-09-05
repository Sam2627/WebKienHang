using WebVanChuyen.Models.Models;

namespace WebVanChuyen.Web.Services
{
    public interface IShipmentTripService
    {
        // ShipmentTrip
        Task<ShipmentTrip> CreateShipmentTrip(ShipmentTrip shipmentTrip);
        Task<ShipmentTrip> GetShipmentTrip(int shipmentTripId);
        Task<List<ShipmentTrip>> GetShipmentTrips();
        Task<List<ShipmentTrip>> GetShipmentTrips(string warehouseId);
        Task<List<ShipmentTrip>> GetShipmentTrips(string warehouseId, bool isStart);
        Task<List<ShipmentTrip>> GetShipmentTripsByDriver(int employeeId);
        Task UpdateShipmentTrip(ShipmentTrip updatedShipment);
        Task UpdateShipmentTrip(UpdateShipmentTripStatus updatedShipment);


        // Truck
        Task<List<Truck>> GetTrucks();
        Task<List<Truck>> GetTrucksByWarehouse(string warehouseId);
    }
}
