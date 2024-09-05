using Microsoft.AspNetCore.Mvc;
using WebVanChuyen.Api.Repositorys;
using WebVanChuyen.Models.Models;

namespace WebVanChuyen.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseRepository _res;

        public WarehouseController(IWarehouseRepository warehouseRepository) { _res = warehouseRepository; }

        // Warehouse
        [HttpPost("warehouse-add")]
        public async Task<ActionResult<Warehouse>> CreateWarehouse(Warehouse warehouse)
        {
            try { return await _res.CreateWarehouse(warehouse); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new data record"); }
        }

        [HttpGet("warehouse/{warehouseId}")]
        public async Task<ActionResult<Warehouse>> GetWarehouse(string warehouseId)
        {
            try { return await _res.GetWarehouse(warehouseId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("warehouse-all")]
        public async Task<ActionResult<IEnumerable<Warehouse>>> GetWarehouses()
        {
            try { return await _res.GetWarehouses(); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("warehouse/province:{districtId}")]
        public async Task<ActionResult<IQueryable<Warehouse>>> GetWarehouses(string provinceId)
        {
            try { return await _res.GetWarehouses(provinceId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("warehouse/adjacent:{warehouseId}")]
        public async Task<ActionResult<IQueryable<Warehouse>>> GetAdjacentWarehouses(string warehouseId)
        {
            try { return await _res.GetAdjacentWarehouses(warehouseId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpPut("warehouse-update")]
        public async Task<ActionResult<Warehouse>> UpdateWarehouse(Warehouse updateWarehouse)
        {
            try { return await _res.UpdateWarehouse(updateWarehouse); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data"); }
        }

        [HttpDelete("warehouse-delele:{warehouseId}")]
        public async Task<ActionResult<Warehouse>> DeleteWarehouse(string warehouseId)
        {
            try { return await _res.DeleteWarehouse(warehouseId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data"); }
        }


        // Province
        [HttpPost("province-add")]
        public async Task<ActionResult<Province>> CreateProvince(Province province)
        {
            try { return await _res.CreateProvince(province); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new data record"); }
        }

        [HttpGet("province/{provinceId}")]
        public async Task<ActionResult<Province>> GetProvince(string provinceId)
        {
            try { return await _res.GetProvince(provinceId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("province-all")]
        public async Task<ActionResult<IEnumerable<Province>>> GetProvinces()
        {
            try { return await _res.GetProvinces(); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("province/warehouse:{warehouseId}")]
        public async Task<ActionResult<IQueryable<Province>>> GetProvinces(string warehouseId)
        {
            try { return await _res.GetProvinces(warehouseId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }


        // District
        [HttpPost("district-add")]
        public async Task<ActionResult<District>> CreateDistrict(District district)
        {
            try { return await _res.CreateDistrict(district); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new data record"); }
        }

        [HttpGet("district/{districtId}")]
        public async Task<ActionResult<District>> GetDistrict(int districtId)
        {
            try { return await _res.GetDistrict(districtId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("district-all")]
        public async Task<ActionResult<IEnumerable<District>>> GetDistricts()
        {
            try { return await _res.GetDistricts(); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("district/warehouse:{warehouseId}")]
        public async Task<ActionResult<IQueryable<District>>> GetDistrictsByWarehouse(string warehouseId)
        {
            try { return await _res.GetDistrictsByWarehouse(warehouseId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("district/province:{provinceId}")]
        public async Task<ActionResult<IQueryable<District>>> GetDistrictsByProvince(string provinceId)
        {
            try { return await _res.GetDistrictsByProvince(provinceId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }


        // Commune
        [HttpGet("commune/warehouse:{warehouseId}")]
        public async Task<ActionResult<IQueryable<Commune>>> GetCommunesByWarehouse(string warehouseId)
        {
            try { return await _res.GetCommunesByWarehouse(warehouseId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("commune/district:{districtId}")]
        public async Task<ActionResult<IQueryable<Commune>>> GetCommunesByDistrict(int districtId)
        {
            try { return await _res.GetCommunesByDistrict(districtId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }
    }
}
