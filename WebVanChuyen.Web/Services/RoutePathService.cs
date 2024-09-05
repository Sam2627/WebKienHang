using System.Net.Http;
using System.Runtime.Intrinsics.X86;
using WebVanChuyen.Models.Models;

namespace WebVanChuyen.Web.Services
{
    public class RoutePathService : IRoutePathService
    {
        private readonly HttpClient _httpClient;
        public RoutePathService(HttpClient httpClient) { _httpClient = httpClient; }


        // Route
        public async Task<WHRoute?> CreateRoute(StartAndEnd snE)
        {
            using HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/route:add", snE);
            if(response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<WHRoute>();

                return result;
            }

            return null;
        }

        public async Task CreateRoutesByWarehouse(StartAndEnd snE)
        {
            await _httpClient.PostAsJsonAsync("api/route:add-list", snE);
        }

        public async Task<WHRoute> GetRoute(int routeId)
        {
            return await _httpClient.GetFromJsonAsync<WHRoute>($"api/route/{routeId}");
        }

        public async Task<WHRoute> GetRoute(string start, string end)
        {
            HttpRequestMessage request = new(HttpMethod.Get, $"api/route/warehouse:{start}@{end}");
            using var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode) return await response.Content.ReadAsAsync<WHRoute>();

            return null;
        }

        public async Task<List<WHRoute>> GetRoutes()
        {
            return await _httpClient.GetFromJsonAsync<List<WHRoute>>("api/route:all");
        }


        // Path
        public async Task CreatePath(WHPath wHPath)
        {
            await _httpClient.PostAsJsonAsync("api/path:add", wHPath);
        }

        public async Task<WHPath> GetPath(int wHPathId)
        {
            return await _httpClient.GetFromJsonAsync<WHPath>($"api/path/{wHPathId}");
        }

        public async Task<WHPath> GetPath(string start, string end)
        {
            HttpRequestMessage request = new(HttpMethod.Get, $"api/path/warehouse:{start}@{end}");
            using var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode) return await response.Content.ReadAsAsync<WHPath>();

            return null;
        }

        public async Task<List<WHPath>> GetPaths()
        {
            return await _httpClient.GetFromJsonAsync<List<WHPath>>("api/path:all");
        }
    }
}
