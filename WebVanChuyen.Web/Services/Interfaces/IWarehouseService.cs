using WebVanChuyen.Models.Models;

namespace WebVanChuyen.Web.Services
{
    public interface IWarehouseService
    {
        // Warehouse
        Task CreateWarehouse(Warehouse warehouse);
        Task<Warehouse> GetWarehouse(string warehouseId);
        Task<List<Warehouse>> GetWarehouses();
        Task<List<Warehouse>> GetWarehouses(string provinceId);
        Task<List<Warehouse>> GetAdjacentWarehouses(string warehouseId);
        Task<bool> CheckWarehouseExist(string warehouseId);
        Task UpdateWarehouse(Warehouse updatedWarehouse);
        Task DeleteWarehouse(string warehouseId);


        // Province
        Task CreateProvince(Province province);
        Task<Province> GetProvince(string provinceId);
        Task<List<Province>> GetProvinces();
        Task<List<Province>> GetProvinces(string warehouseId);
        

        // District
        Task CreateDistrict(District districtId);
        Task<District> GetDistrict(int districtId);
        Task<List<District>> GetDistricts();
        Task<List<District>> GetDistrictsByWarehouse(string warehouseId);
        Task<List<District>> GetDistrictsByProvince(string provinceId);


        // Commune
        Task<List<Commune>> GetCommunesByWarehouse(string warehouseId);
        Task<List<Commune>> GetCommunesByDistrict(int districtId);
    }
}
