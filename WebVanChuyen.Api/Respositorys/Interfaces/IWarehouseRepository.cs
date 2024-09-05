using Microsoft.AspNetCore.Mvc;
using WebVanChuyen.Models.Models;

namespace WebVanChuyen.Api.Repositorys
{
    public interface IWarehouseRepository
    {
        // Warehouse
        Task<ActionResult<Warehouse>> CreateWarehouse(Warehouse warehouse);
        Task<ActionResult<Warehouse>> GetWarehouse(string warehouseId);
        Task<ActionResult<IEnumerable<Warehouse>>> GetWarehouses();
        Task<ActionResult<IQueryable<Warehouse>>> GetWarehouses(string provinceId);
        Task<ActionResult<IQueryable<Warehouse>>> GetAdjacentWarehouses(string warehouseId);
        Task<ActionResult<Warehouse>> UpdateWarehouse(Warehouse updateWarehouse);
        Task<ActionResult<Warehouse>> DeleteWarehouse(string warehouseId);


        // Province
        Task<ActionResult<Province>> CreateProvince(Province province);
        Task<ActionResult<Province>> GetProvince(string provinceId);
        Task<ActionResult<IEnumerable<Province>>> GetProvinces();
        Task<ActionResult<IQueryable<Province>>> GetProvinces(string warehouseId);


        // District
        Task<ActionResult<District>> CreateDistrict(District district);
        Task<ActionResult<District>> GetDistrict(int districtId);
        Task<ActionResult<IEnumerable<District>>> GetDistricts();
        Task<ActionResult<IQueryable<District>>> GetDistrictsByWarehouse(string warehouseId);
        Task<ActionResult<IQueryable<District>>> GetDistrictsByProvince(string provinceId);


        // Commune
        Task<ActionResult<IQueryable<Commune>>> GetCommunesByWarehouse(string warehouseId);
        Task<ActionResult<IQueryable<Commune>>> GetCommunesByDistrict(int districtId);
    }
}
