using Microsoft.AspNetCore.Mvc;
using WebVanChuyen.Api.Repositorys;
using WebVanChuyen.Models.Models;

namespace WebVanChuyen.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _res;

        public EmployeesController(IEmployeeRepository employeeRepository) { _res = employeeRepository; }


        // Employee
        [HttpPost("employee:add")]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            try { return await _res.CreateEmployee(employee); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new data record"); }
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<Employee>> GetEmployee(int employeeId)
        {
            try { return await _res.GetEmployee(employeeId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("employee:all")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            try { return await _res.GetEmployees(); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("employee/warehouse:{warehouseId}")]
        public async Task<ActionResult<IQueryable<Employee>>> GetEmployeeByWarehouse(string warehouseId)
        {
            try { return await _res.GetEmployeeByWarehouse(warehouseId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("employee/position:{positionId}")]
        public async Task<ActionResult<IQueryable<Employee>>> GetEmployeesByPosition(int positionId)
        {
            try { return await _res.GetEmployeesByPosition(positionId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("employee/package:{packageId}")]
        public async Task<ActionResult<IQueryable<Employee>>> GetEmployeesByPackage(int packageId)
        {
            try { return await _res.GetEmployeesByPosition(packageId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpPut("employee:update")]
        public async Task<ActionResult<Employee>> UpdateEmployee(Employee employee)
        {
            try { return await _res.UpdateEmployee(employee); }
            catch (Exception) {  return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data"); }
        }

        [HttpDelete("employee:delete/{employeeId}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int employeeId)
        {
            try { return await _res.DeleteEmployee(employeeId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data"); }
        }

        /*
        [HttpPatch("{employeeId:int}")]
        public async Task<ActionResult<Employee>> PatchEmployee(int employeeId, [FromBody] JsonPatchDocument employeeDoc)
        {
            return await _employeeRepository.PatchEmployee(employeeId, employeeDoc);
        }
        */


        // Employee : Driver
        [HttpGet("driver:all")]
        public async Task<ActionResult<IQueryable<Employee>>> GetDrivers()
        {
            try { return await _res.GetDrivers(); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("driver/warehouse:{warehouseId}")]
        public async Task<ActionResult<IQueryable<Employee>>> GetDriversByWarehouse(string warehouseId)
        {
            try { return await _res.GetDriversByWarehouse(warehouseId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("driver/location:{warehouseId}")]
        public async Task<ActionResult<IQueryable<Employee>>> GetDriversByLocation(string warehouseId)
        {
            try { return await _res.GetDriversByLocation(warehouseId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpPut("driver:update-location")]
        public async Task<ActionResult<Employee>> UpdateDriverLocation(EmployeeLocation enL)
        {
            try { return await _res.UpdateDriverLocation(enL); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data"); }
        }


        // EmployeePosition
        [HttpGet("employeeposition:all")]
        public async Task<ActionResult<IEnumerable<EmployeePosition>>> GetEmployeePositions()
        {
            try { return await _res.GetEmployeePositions(); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }


        // EmployeeInfo
        [HttpGet("employee-info/{employeeId}")]
        public async Task<ActionResult<EmployeeInfo>> GetEmployeeInfoById(int employeeId)
        {
            try { return await _res.GetEmployeeInfoById(employeeId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }
    }
}
