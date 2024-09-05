using Microsoft.AspNetCore.Mvc;
using WebVanChuyen.Api.Repositorys;
using WebVanChuyen.Models.Models;


namespace WebVanChuyen.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ShipmentTripController : ControllerBase
    {
        private readonly IShipmentTripResposity _res;

        public ShipmentTripController(IShipmentTripResposity shipmentTripRepository) { _res = shipmentTripRepository; }

        // ShipmentTrip
        [HttpPost("shipment-trip:add")]
        public async Task<ActionResult<ShipmentTrip>> CreateShipmentTrip(ShipmentTrip shipmentTrip)
        {
            try { return await _res.CreateShipmentTrip(shipmentTrip); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new data record"); }
        }

        [HttpGet("shipment-trip/{shipmentTripId}")]
        public async Task<ActionResult<ShipmentTrip>> GetShipmentTrip(int shipmentTripId)
        {
            try { return await _res.GetShipmentTrip(shipmentTripId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("shipment-trip:all")]
        public async Task<ActionResult<IEnumerable<ShipmentTrip>>> GetShipmentTrips()
        {
            try { return await _res.GetShipmentTrips(); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("shipment-trip/warehouse:{warehouseId}")]
        public async Task<ActionResult<IQueryable<ShipmentTrip>>> GetShipmentTrips(string warehouseId)
        {
            try { return await _res.GetShipmentTrips(warehouseId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("shipment-trip/warehouse:{warehouseId}/start:{isStart}")]
        public async Task<ActionResult<IQueryable<ShipmentTrip>>> GetShipmentTrips(string warehouseId, bool isStart)
        {
            try { return await _res.GetShipmentTrips(warehouseId, isStart); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("shipment-trip/driver:{employeeId}")]
        public async Task<ActionResult<IQueryable<ShipmentTrip>>> GetAllShipmentTripsByDriver(int employeeId)
        {
            try { return await _res.GetShipmentTripsByDriver(employeeId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpPut("shipment-trip:update")]
        public async Task<ActionResult<ShipmentTrip>> UpdateShipmentTrip(ShipmentTrip updateShipmentTrip)
        {
            try { return await _res.UpdateShipmentTrip(updateShipmentTrip); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error update data to the database"); }
        }

        [HttpPut("shipment-trip:update-status")]
        public async Task<ActionResult<ShipmentTrip>> UpdateShipmentTripStatus(UpdateShipmentTripStatus updateShipmentTrip)
        {
            try { return await _res.UpdateShipmentTripStatus(updateShipmentTrip); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error update data to the database"); }
        }


        // Truck
        [HttpGet("truck:all")]
        public async Task<ActionResult<IEnumerable<Truck>>> GetTrucks()
        {
            try { return await _res.GetTrucks(); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("truck/warehouse:{warehouseId}")]
        public async Task<ActionResult<IQueryable<Truck>>> GetTrucks(string warehouseId)
        {
            try { return await _res.GetTrucks(warehouseId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }
    }
}
