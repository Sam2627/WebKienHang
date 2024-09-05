using WebVanChuyen.Models.Models;

namespace WebVanChuyen.Web.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly HttpClient _httpClient;
        public WarehouseService(HttpClient httpClient) { _httpClient = httpClient; }


        // Warehouse
        public async Task CreateWarehouse(Warehouse warehouse)
        {
            await _httpClient.PostAsJsonAsync("api/warehouse-add", warehouse);
        }

        public async Task<Warehouse> GetWarehouse(string warehouseId)
        {
            return await _httpClient.GetFromJsonAsync<Warehouse>($"api/warehouse/{warehouseId}");

        }

        public async Task<List<Warehouse>> GetWarehouses()
        {
            return await _httpClient.GetFromJsonAsync<List<Warehouse>>("api/warehouse-all");
        }

        public async Task<List<Warehouse>> GetWarehouses(string provinceId)
        {
            return await _httpClient.GetFromJsonAsync<List<Warehouse>>($"api/warehouse/province:{provinceId}");
        }

        public async Task<List<Warehouse>> GetAdjacentWarehouses(string warehouseId)
        {
            return await _httpClient.GetFromJsonAsync<List<Warehouse>>($"api/warehouse/adjacent:{warehouseId}");
        }

        public async Task UpdateWarehouse(Warehouse updatedWarehouse)
        {
            await _httpClient.PutAsJsonAsync("api/warehouse-update", updatedWarehouse);
        }

        public async Task DeleteWarehouse(string warehouseId)
        {
            await _httpClient.DeleteAsync($"api/warehouse-delete:{warehouseId}");
        }

        public async Task<bool> CheckWarehouseExist(string warehouseId)
        {
            HttpRequestMessage request = new(HttpMethod.Get, $"api/warehouse/{warehouseId}");
            using var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode) return true;

            return false;
        }


        // Province
        public async Task CreateProvince(Province province)
        {
            await _httpClient.PostAsJsonAsync("api/province-add", province);
        }

        public async Task<Province> GetProvince(string provinceId)
        {
            return await _httpClient.GetFromJsonAsync<Province>($"api/province/{provinceId}");
        }

        public async Task<List<Province>> GetProvinces()
        {
            return await _httpClient.GetFromJsonAsync<List<Province>>("api/province-all");
        }

        public async Task<List<Province>> GetProvinces(string warehouseId)
        {
            return await _httpClient.GetFromJsonAsync<List<Province>>($"api/province/warehouse:{warehouseId}");
        }


        // District
        public async Task CreateDistrict(District district)
        {
            await _httpClient.PostAsJsonAsync("api/district-add", district);
        }

        public async Task<District> GetDistrict(int districtId)
        {
            return await _httpClient.GetFromJsonAsync<District>($"api/district/{districtId}");
        }

        public async Task<List<District>> GetDistricts()
        {
            return await _httpClient.GetFromJsonAsync<List<District>>("api/district-all");
        }

        public async Task<List<District>> GetDistrictsByWarehouse(string warehouseId)
        {
            return await _httpClient.GetFromJsonAsync<List<District>>($"api/district/warehouse:{warehouseId}");
        }

        public async Task<List<District>> GetDistrictsByProvince(string provinceId)
        {
            return await _httpClient.GetFromJsonAsync<List<District>>($"api/district/province:{provinceId}");
        }


        // Commune
        public async Task<List<Commune>> GetCommunesByWarehouse(string warehouseId)
        {
            return await _httpClient.GetFromJsonAsync<List<Commune>>($"api/commune/warehouse:{warehouseId}");
        }

        public async Task<List<Commune>> GetCommunesByDistrict(int districtId)
        {
            return await _httpClient.GetFromJsonAsync<List<Commune>>($"api/commune/district:{districtId}");
        }
    }
}
