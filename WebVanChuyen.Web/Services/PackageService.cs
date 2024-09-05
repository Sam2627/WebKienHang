using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.Intrinsics.X86;
using WebVanChuyen.Models.Models;
using WebVanChuyen.Web.Pages;

namespace WebVanChuyen.Web.Services
{
    public class PackageService : IPackageService
    {
        private readonly HttpClient _httpClient;
        public PackageService(HttpClient httpClient) { _httpClient = httpClient; }

        // Package
        public async Task<Package?> CreatePackage(Package package)
        {
            using HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/package:add", package);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<Package>();

                return result;
            }

            return null;
        }

        public async Task<Package> GetPackage(int packageId)
        {
            using HttpResponseMessage response = await _httpClient.GetAsync($"api/package/{packageId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<Package>();
            }

            return null;
        }

        public async Task<List<Package>> GetPackages()
        {
            return await _httpClient.GetFromJsonAsync<List<Package>>("api/package:all");
        }

        public async Task<List<Package>> GetPackages(string warehouseId, bool exceptComplete)
        {
            return await _httpClient.GetFromJsonAsync<List<Package>>($"api/package/warehouse:{warehouseId}/{exceptComplete}");
        }

        public async Task<List<Package>> GetPackagesCanTransport(string warehouseId)
        {
            return await _httpClient.GetFromJsonAsync<List<Package>>($"api/package/transport/warehouse:{warehouseId}");
        }

        public async Task<List<Package>> GetPackagesCanDelivery(string warehouseId)
        {
            return await _httpClient.GetFromJsonAsync<List<Package>>($"api/package/delivery/warehouse:{warehouseId}");
        }

        public async Task<List<Package>> GetPackagesByShipper(int employeeId)
        {
            return await _httpClient.GetFromJsonAsync<List<Package>>($"api/package/delivery/shipper:{employeeId}");
        }

        public async Task<List<Package>> GetPackagesInShipmentTrip(int shipmentTripId)
        {
            return await _httpClient.GetFromJsonAsync<List<Package>>($"api/package/shipment-trip:{shipmentTripId}");
        }

        public async Task<List<Package>> GetPackageByNextWarehouse(string start, string end)
        {
            return await _httpClient.GetFromJsonAsync<List<Package>>($"api/package/start:{start}/end:{end}");
        }

        public async Task UpdatePackageStatus(Package updatePackage)
        {
            await _httpClient.PutAsJsonAsync($"api/package:update", updatePackage);
        }

        public async Task PackagShipperRegister(PackageRegister registerPackage)
        {
            await _httpClient.PutAsJsonAsync($"api/package:update-register", registerPackage);
        }

        public async Task PackageConfirmDelivery(PackageDelivery confirmPackage)
        {
            await _httpClient.PutAsJsonAsync("api/package:update-delivery", confirmPackage);
        }

        public async Task DeletePackage(int packageId)
        {
            await _httpClient.DeleteAsync($"api/package:delete/{packageId}");
        }


        // PackageLog
        public async Task<PackageLog> AddPackageLog(PackageLog packageLog, bool isCreate)
        {
            using HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/package-log:add/{isCreate}", packageLog);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<PackageLog>();

                return result;
            }

            return null;
        }

        public async Task<List<PackageLog>> GetPackageLogsByPackage(int packageId)
        {
            return await _httpClient.GetFromJsonAsync<List<PackageLog>>($"api/package-log/package:{packageId}");
        }

        public async Task<List<PackageLog>> GetPackageLogsByPhone(string phoneNum)
        {
            return await _httpClient.GetFromJsonAsync<List<PackageLog>>($"api/package-log/phone:{phoneNum}");
        }

        public async Task<List<PackageLog>> GetPackageLogsByShipmentTrip(int shipmentTripId)
        {
            return await _httpClient.GetFromJsonAsync<List<PackageLog>>($"api/package-log/shipment-trip:{shipmentTripId}");
        }

        public async Task DeletePackageLog(int packageLogId)
        {
            await _httpClient.DeleteAsync($"api/package-log:delete/{packageLogId}");
        }


        // PackageWeight
        public async Task CreatePackageWeight(PackageWeight package)
        {
            await _httpClient.PostAsJsonAsync("api/weight:add", package);
        }

        public async Task<PackageWeight> GetPackageWeight(double packageWeightId)
        {
            return await _httpClient.GetFromJsonAsync<PackageWeight>($"api/weight/{packageWeightId}");
        }

        public async Task<List<PackageWeight>> GetPackageWeights()
        {
            return await _httpClient.GetFromJsonAsync<List<PackageWeight>>($"api/weight:all");
        }

        public async Task UpdatePackageWeight(PackageWeight updatepackageWeight)
        {
            await _httpClient.PutAsJsonAsync($"api/weight:update", updatepackageWeight);
        }
    }
}
