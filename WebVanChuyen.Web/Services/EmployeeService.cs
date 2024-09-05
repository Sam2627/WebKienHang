using WebVanChuyen.Models.Models;
using System.Net.Http;
using WebVanChuyen.Web.Pages;

namespace WebVanChuyen.Web.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;
        public EmployeeService(HttpClient httpClient) { _httpClient = httpClient; }

        // Employee
        public async Task CreateEmployee(Employee employee)
        {
            await _httpClient.PostAsJsonAsync("api/employee:add", employee);
        }

        public async Task<Employee> GetEmployee(int employeeId)
        {
            HttpRequestMessage request = new(HttpMethod.Get, $"api/employee/{employeeId}");
            using var response = await _httpClient.SendAsync(request);
            var result = await response.Content.ReadAsAsync<Employee>();
            return result;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await _httpClient.GetFromJsonAsync<List<Employee>>("api/employee:all");
        }

        public async Task<List<Employee>> GetEmployeeByWarehouse(string warehouseId)
        {
            return await _httpClient.GetFromJsonAsync<List<Employee>>($"api/employee/warehouse:{warehouseId}");
        }

        public async Task<List<Employee>> GetEmployeesByPosition(int positionId)
        {
            return await _httpClient.GetFromJsonAsync<List<Employee>>($"api/employee/position:{positionId}");
        }

        public async Task<List<Employee>> GetEmployeesByPackage(int packageId)
        {
            return await _httpClient.GetFromJsonAsync<List<Employee>>($"api/employee/package:{packageId}");
        }

        public async Task UpdateEmployee(Employee updateEmployee)
        {
            await _httpClient.PutAsJsonAsync("api/employee:update", updateEmployee);
        }

        public async Task DeleteEmployee(int employeeId)
        {
            await _httpClient.DeleteAsync($"api/employee:delete/{employeeId}");
        }


        // Employee : Driver
        public async Task<List<Employee>> GetDrivers()
        {
            return await _httpClient.GetFromJsonAsync<List<Employee>>("api/driver:all");
        }

        public async Task<List<Employee>> GetDriversByWarehouse(string warehouseId)
        {
            return await _httpClient.GetFromJsonAsync<List<Employee>>($"api/driver/warehouse:{warehouseId}");
        }

        public async Task<List<Employee>> GetDriverByLocation(string warehouseId)
        {
            return await _httpClient.GetFromJsonAsync<List<Employee>>($"api/driver/location:{warehouseId}");
        }

        public async Task UpdateDriverLocation(EmployeeLocation enL)
        {
            await _httpClient.PutAsJsonAsync("api/driver:update-location", enL);
        }


        // EmployeePosition
        public async Task<List<EmployeePosition>> GetEmployeePositions()
        {
            return await _httpClient.GetFromJsonAsync<List<EmployeePosition>>("api/employeeposition:all");
        }


        // Employee Info
        public async Task<EmployeeInfo> GetEmployeeInfo(int employeeId)
        {
            return await _httpClient.GetFromJsonAsync<EmployeeInfo>($"api/employee-info/{employeeId}");
        }
    }
}
