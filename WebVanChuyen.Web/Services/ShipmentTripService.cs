using System.Net.Http;
using System.Runtime.Intrinsics.X86;
using WebVanChuyen.Models.Models;

namespace WebVanChuyen.Web.Services
{
    public class ShipmentTripService : IShipmentTripService
    {
        private readonly HttpClient _httpClient;
        public ShipmentTripService(HttpClient httpClient) { _httpClient = httpClient; }

        // ShipmentTrip
        public async Task<ShipmentTrip> CreateShipmentTrip(ShipmentTrip shipmentTrip)
        {
            using HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/shipment-trip:add", shipmentTrip);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<ShipmentTrip>();

                return result;
            }

            return null;
        }

        public async Task<ShipmentTrip> GetShipmentTrip(int shipmentTripId)
        {
            return await _httpClient.GetFromJsonAsync<ShipmentTrip>($"api/shipment-trip/{shipmentTripId}");
        }

        public async Task<List<ShipmentTrip>> GetShipmentTrips()
        {
            return await _httpClient.GetFromJsonAsync<List<ShipmentTrip>>("api/shipment-trip:all");
        }

        public async Task<List<ShipmentTrip>> GetShipmentTrips(string warehouseId)
        {
            return await _httpClient.GetFromJsonAsync<List<ShipmentTrip>>($"api/shipment-trip/warehouse:{warehouseId}");
        }

        public async Task<List<ShipmentTrip>> GetShipmentTrips(string warehouseId, bool isStart)
        {
            return await _httpClient.GetFromJsonAsync<List<ShipmentTrip>>($"api/shipment-trip/warehouse:{warehouseId}/start:{isStart}");
        }

        public async Task<List<ShipmentTrip>> GetShipmentTripsByDriver(int employeeId)
        {
            return await _httpClient.GetFromJsonAsync<List<ShipmentTrip>>($"api/shipment-trip/driver:{employeeId}");
        }

        public async Task UpdateShipmentTrip(ShipmentTrip updatedShipmentTrip)
        {
            await _httpClient.PutAsJsonAsync("api/shipment-trip:update", updatedShipmentTrip);
        }

        public async Task UpdateShipmentTrip(UpdateShipmentTripStatus updatedShipment)
        {
            await _httpClient.PutAsJsonAsync("api/shipment-trip:update-status", updatedShipment);
        }


        // Truck
        public async Task<List<Truck>> GetTrucks()
        {
            using HttpResponseMessage response = await _httpClient.GetAsync("api/truck:all");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<List<Truck>>();

                return result;
            }

            return null;
        }

        public async Task<List<Truck>> GetTrucksByWarehouse(string warehouseId)
        {
            using HttpResponseMessage response = await _httpClient.GetAsync($"api/truck/warehouse:{warehouseId}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<List<Truck>>();

                return result;
            }

            return null;
        }
    }
}
