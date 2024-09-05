using Microsoft.AspNetCore.Mvc;
using WebVanChuyen.Models.Models;

namespace WebVanChuyen.Web.Services
{
    public interface IEmployeeService
    {
        // Employee
        Task CreateEmployee(Employee employee);
        Task<Employee> GetEmployee(int employeeId);
        Task<List<Employee>> GetEmployees();
        Task<List<Employee>> GetEmployeeByWarehouse(string warehouseId);
        Task<List<Employee>> GetEmployeesByPosition(int positionId);
        Task<List<Employee>> GetEmployeesByPackage(int packageId);
        Task UpdateEmployee(Employee updatedEmployee);
        Task DeleteEmployee(int employeeId);


        // Employee : Driver
        Task<List<Employee>> GetDrivers();
        Task<List<Employee>> GetDriversByWarehouse(string warehouseId);
        Task<List<Employee>> GetDriverByLocation(string warehouseId);
        Task UpdateDriverLocation(EmployeeLocation enL);


        // EmployeePosition
        Task<List<EmployeePosition>> GetEmployeePositions();


        // EmployeeInfo
        Task<EmployeeInfo> GetEmployeeInfo(int employeeId);
    }
}

    