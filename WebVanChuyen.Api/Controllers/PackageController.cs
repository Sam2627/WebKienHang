using Microsoft.AspNetCore.Mvc;
using WebVanChuyen.Api.Repositorys;
using WebVanChuyen.Models.Models;

namespace WebVanChuyen.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IPackageRespository _res;

        public PackageController(IPackageRespository packageRepository) { _res = packageRepository; }

        // Package
        [HttpPost("package:add")]
        public async Task<ActionResult<Package>> CreatePackage(Package newPackage)
        {
            try { return await _res.CreatePackage(newPackage); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new data record"); }
        }

        [HttpGet("package/{packageId}")]
        public async Task<ActionResult<Package>> GetPackage(int packageId)
        {
            try { return await _res.GetPackage(packageId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("package:all")]
        public async Task<ActionResult<IEnumerable<Package>>> GetPackages()
        {
            try { return await _res.GetPackages(); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("package/warehouse:{warehouseId}/{exceptComplete}")]
        public async Task<ActionResult<IQueryable<Package>>> GetPackages(string warehouseId, bool exceptComplete)
        {
            try { return await _res.GetPackages(warehouseId, exceptComplete); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("package/transport/warehouse:{warehouseId}")]
        public async Task<ActionResult<IQueryable<Package>>> GetPackagesCanTransport(string warehouseId)
        {
            try { return await _res.GetPackagesCanTransport(warehouseId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("package/delivery/warehouse:{warehouseId}")]
        public async Task<ActionResult<IQueryable<Package>>> GetPackagesCanDelivery(string warehouseId)
        {
            try { return await _res.GetPackagesCanDelivery(warehouseId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("package/delivery/shipper:{employeeId}")]
        public async Task<ActionResult<IQueryable<Package>>> GetPackagesByShipper(int employeeId)
        {
            try { return await _res.GetPackagesByShipper(employeeId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("package/shipment-trip:{shipmentTripId}")]
        public async Task<ActionResult<IQueryable<Package>>> GetPackagesInShipmentTrip(int shipmentTripId)
        {
            try { return await _res.GetPackagesInShipmentTrip(shipmentTripId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("package/start:{snE.StartPoint}/end:{snE.EndPoint}")]
        public async Task<ActionResult<List<Package>>> GetPackageByNextWarehouse(string start, string end)
        {
            try { return await _res.GetPackagesByNextWarehouse(start, end); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpPut("package:update")]
        public async Task<ActionResult<Package>> UpdatePackageStatus(Package updatePackage)
        {
            try { return await _res.UpdatePackage(updatePackage); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error update data to the database"); }
        }

        [HttpPut("package:update-register")]
        public async Task<ActionResult> PackagShipperRegister(PackageRegister registerPackage)
        {
            try { return await _res.PackagShipperRegister(registerPackage); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error update data to the database"); }
        }

        [HttpPut("package:update-delivery")]
        public async Task<ActionResult<Package>> PackageConfirmDelivery(PackageDelivery confirmPackage)
        {
            try { return await _res.PackageConfirmDelivery(confirmPackage); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error update data to the database"); }
        }

        [HttpDelete("package:delete/{packageId}")]
        public async Task<ActionResult<Package>> DeletePackage(int packageId)
        {
            try { return await _res.DeletePackage(packageId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data"); }
        }


        // PackageLog
        [HttpPost("package-log:add/{isCreate}")]
        public async Task<ActionResult<PackageLog>> AddPackageLog(PackageLog packageLog, bool isCreate)
        {
            try { return await _res.AddPackageLog(packageLog, isCreate); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new data record"); }
        }

        [HttpGet("package-log:all")]
        public async Task<ActionResult<IEnumerable<PackageLog>>> GetAllPackageLog()
        {
            try { return await _res.GetAllPackageLog(); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("package-log/package:{packageId}")]
        public async Task<ActionResult<IQueryable<PackageLog>>> GetPackageLogsByPackage(int packageId)
        {
            try { return await _res.GetPackageLogsByPackage(packageId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("package-log/phone:{phoneNum}")]
        public async Task<ActionResult<IQueryable<PackageLog>>> GetPackageLogsByPhone(string phoneNum)
        {
            try { return await _res.GetPackageLogsByPhone(phoneNum); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("package-log/shipment-trip:{shipmentTripId}")]
        public async Task<ActionResult<IQueryable<PackageLog>>> GetPackageLogsByShipmentTrip(int shipmentTripId)
        {
            try { return await _res.GetPackageLogsByShipmentTrip(shipmentTripId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpDelete("package-log:delete/{packageLogId}")]
        public async Task<ActionResult<PackageLog>> DeletePackageLog(int packageLogId)
        {
            try { return await _res.DeletePackageLog(packageLogId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data"); }
        }


        // PackageWeight
        [HttpPost("weight:add")]
        public async Task<ActionResult<PackageWeight>> CreatePackageWeight(PackageWeight packageWeight)
        {
            try { return await _res.CreatePackageWeight(packageWeight); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new data record"); }
        }

        [HttpGet("weight/{packageWeightId}")]
        public async Task<ActionResult<PackageWeight>> GetPackageWeightById(double packageWeightId)
        {
            try { return (await _res.GetPackageWeightById(packageWeightId)); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("weight:all")]
        public async Task<ActionResult<IEnumerable<PackageWeight>>> GetAllPackageWeights()
        {
            try { return await _res.GetAllPackageWeights(); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpPut("weight:update")]
        public async Task<ActionResult<PackageWeight>> UpdatePackageWeight(PackageWeight updatepackageWeight)
        {
            try { return await _res.UpdatePackageWeight(updatepackageWeight); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error update data to the database"); }
        }
    }
}
