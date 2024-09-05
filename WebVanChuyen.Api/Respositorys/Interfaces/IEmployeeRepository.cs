using Microsoft.AspNetCore.Mvc;
using WebVanChuyen.Models.Models;

namespace WebVanChuyen.Api.Repositorys
{
    public interface IEmployeeRepository
    {

        // Employee
        Task<ActionResult<Employee>> CreateEmployee(Employee employee);
        Task<ActionResult<Employee>> GetEmployee(int employeeId);
        Task<ActionResult<IEnumerable<Employee>>> GetEmployees();
        Task<ActionResult<IQueryable<Employee>>> GetEmployeeByWarehouse(string warehouseId);
        Task<ActionResult<IQueryable<Employee>>> GetEmployeesByPosition(int positionId);
        Task<ActionResult<IQueryable<Employee>>> GetEmployeesByPackage(int packageId);
        Task<ActionResult<Employee>> UpdateEmployee(Employee employee);
        Task<ActionResult<Employee>> DeleteEmployee(int employeeId);

        //Task<ActionResult<Employee>> PatchEmployee(int employeeId, JsonPatchDocument employeeDoc);


        // Employee : Driver
        Task<ActionResult<IQueryable<Employee>>> GetDrivers();
        Task<ActionResult<IQueryable<Employee>>> GetDriversByWarehouse(string warehouseId);
        Task<ActionResult<IQueryable<Employee>>> GetDriversByLocation(string warehouseId);
        Task<ActionResult<Employee>> UpdateDriverLocation(EmployeeLocation enL);


        // EmployeePosition
        Task<ActionResult<IEnumerable<EmployeePosition>>> GetEmployeePositions();


        // EmployeeInfo
        Task<ActionResult<EmployeeInfo>> GetEmployeeInfoById(int employeeId);
    }
}
