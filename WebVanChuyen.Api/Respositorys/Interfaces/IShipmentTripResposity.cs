using Microsoft.AspNetCore.Mvc;
using WebVanChuyen.Models.Models;

namespace WebVanChuyen.Api.Repositorys
{
    public interface IShipmentTripResposity
    {
        // ShipmentTrip
        Task<ActionResult<ShipmentTrip>> CreateShipmentTrip(ShipmentTrip shipmentTrip);
        Task<ActionResult<ShipmentTrip>> GetShipmentTrip(int shipmentTripId);
        Task<ActionResult<IEnumerable<ShipmentTrip>>> GetShipmentTrips();
        Task<ActionResult<IQueryable<ShipmentTrip>>> GetShipmentTrips(string warehouseId);
        Task<ActionResult<IQueryable<ShipmentTrip>>> GetShipmentTrips(string warehouseId, bool isStart);
        Task<ActionResult<IQueryable<ShipmentTrip>>> GetShipmentTripsByDriver(int employeeId);
        Task<ActionResult<ShipmentTrip>> UpdateShipmentTrip(ShipmentTrip updateShipmentTrip);
        Task<ActionResult<ShipmentTrip>> UpdateShipmentTripStatus(UpdateShipmentTripStatus shipmentTrip);


        // 
        Task<ActionResult<IEnumerable<Truck>>> GetTrucks();
        Task<ActionResult<IQueryable<Truck>>> GetTrucks(string warehouseId);
    }
}
