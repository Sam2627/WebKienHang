using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVanChuyen.Api.Data;
using WebVanChuyen.Models.Models;

namespace WebVanChuyen.Api.Repositorys
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContextWeb appDbContext;

        public EmployeeRepository(DataContextWeb appDbContext) { this.appDbContext = appDbContext; }


        // Employee
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            if (employee == null) return new BadRequestResult(); 

            employee.LocaId = employee.WWarehouseId;      // Set local

            var result = await appDbContext.Employee.AddAsync(employee);
            await appDbContext.SaveChangesAsync();

            return new CreatedResult(string.Empty, result.Entity);
        }

        public async Task<ActionResult<Employee>> GetEmployee(int employeeId)
        {
            //var result = await appDbContext.Employees.Include(e => e.Warehouse).Include(e => e.Position).FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
            var result = await appDbContext.Employee.AsQueryable().FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
            if (result != null) return new OkObjectResult(result);

            //return new NotFoundObjectResult($"Not found employee ID: {employeeId}");
            return new NotFoundResult();
        }

        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            //var result = await appDbContext.Employee.Include(p => p.Position).ToListAsync();
            var result = await appDbContext.Employee.ToListAsync();

            return new OkObjectResult(result); 
        }

        public async Task<ActionResult<IQueryable<Employee>>> GetEmployeeByWarehouse(string warehouseId)
        {
            var warehouse = await appDbContext.Warehouse.AsQueryable().FirstOrDefaultAsync(w => w.WarehouseId == warehouseId);
            if (warehouse == null) { return new BadRequestResult(); }

            var result = await appDbContext.Employee.AsQueryable().Where(w => w.WWarehouseId == warehouseId).ToListAsync();
            
            return new OkObjectResult(result);
        }

        public async Task<ActionResult<IQueryable<Employee>>> GetEmployeesByPosition(int positionId)
        {
            var result = await appDbContext.Employee.AsQueryable().Where(e => e.PositionId == positionId).ToListAsync();
            
            return new OkObjectResult(result);
        }

        public async Task<ActionResult<IQueryable<Employee>>> GetEmployeesByPackage(int packageId)
        {
            var package = await appDbContext.Package.AsQueryable().FirstOrDefaultAsync(package => package.PackageId == packageId);
            if(package == null) return new NotFoundResult();

            var result = await appDbContext.Employee.AsQueryable().Where(
                emp => emp.EmployeeId == package.ReceptionistId || emp.EmployeeId == package.ShipperId).ToListAsync();

            return new OkObjectResult(result);
        }

        public async Task<ActionResult<Employee>> UpdateEmployee(Employee employee)
        {
            var result = await appDbContext.Employee.AsQueryable().FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);

            if (result != null)
            {
                result.EmployeeName = employee.EmployeeName;
                result.PositionId = employee.PositionId;
                result.WWarehouseId = employee.WWarehouseId;

                await appDbContext.SaveChangesAsync();

                return new OkObjectResult(result);
            }

            return new BadRequestResult();
        }

        public async Task<ActionResult<Employee>> DeleteEmployee(int employeeId)
        {
            var result = await appDbContext.Employee.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
            if (result != null)
            {
                appDbContext.Employee.Remove(result);
                await appDbContext.SaveChangesAsync();

                return new OkObjectResult(result);
            }

            return new BadRequestResult();
        }

        /*
        public async Task<ActionResult<Employee>> PatchEmployee(int employeeId, JsonPatchDocument employeeDoc)
        {
            var result = await GetEmployee(employeeId);

            if (result == null) return new BadRequestResult();

            employeeDoc.ApplyTo(result);
            await appDbContext.SaveChangesAsync();

            return new OkObjectResult(result);
        }
        */


        // Employee : Driver
        public async Task<ActionResult<IQueryable<Employee>>> GetDrivers()
        {
            var result = await appDbContext.Employee.AsQueryable().Where(d => d.PositionId == 4).ToListAsync();
            
            return new OkObjectResult(result);
        }

        public async Task<ActionResult<IQueryable<Employee>>> GetDriversByWarehouse(string warehouseId)
        {
            var warehouse = await appDbContext.Warehouse.AsQueryable().FirstOrDefaultAsync(w => w.WarehouseId == warehouseId);
            if (warehouse == null) { return new BadRequestObjectResult($"Not found warehouse ID: {warehouseId}"); }

            var result = await appDbContext.Employee.AsQueryable().Where(d => d.WWarehouseId == warehouseId && d.PositionId == 4).ToListAsync();
            
            return new OkObjectResult(result);
        }

        public async Task<ActionResult<IQueryable<Employee>>> GetDriversByLocation(string warehouseId)
        {
            var warehouse = await appDbContext.Warehouse.AsQueryable().FirstOrDefaultAsync(w => w.WarehouseId == warehouseId);
            if (warehouse == null) { return new BadRequestResult(); }

            var result = await appDbContext.Employee.AsQueryable().Where(d => d.LocaId == warehouseId && d.PositionId == 4).ToListAsync();
            
            return new OkObjectResult(result);
        }

        public async Task<ActionResult<Employee>> UpdateDriverLocation(EmployeeLocation enL)
        {
            var result = await appDbContext.Employee.AsQueryable().FirstOrDefaultAsync(e => e.EmployeeId == enL.Employee);
            if (result != null)
            {
                result.LocaId = enL.Warehouse;
                await appDbContext.SaveChangesAsync();

                return new OkObjectResult(result);
            }

            return new BadRequestResult();
        }


        // EmployeePosition
        public async Task<ActionResult<IEnumerable<EmployeePosition>>> GetEmployeePositions()
        {
            var result = await appDbContext.EmployeePosition.ToListAsync();
            
            return new OkObjectResult(result);
        }


        // EmployeeInfo
        public async Task<ActionResult<EmployeeInfo>> GetEmployeeInfoById(int employeeId)
        {
            if (employeeId == 0) return new BadRequestResult();

            var employee = await appDbContext.Employee.AsQueryable().FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
            if (employee != null)
            {
                var employeePosition = (await appDbContext.EmployeePosition.FirstOrDefaultAsync(emp => emp.PositionId == employee.PositionId)).PositionId;

                EmployeeInfo userInfo = new EmployeeInfo()
                {
                    EmployeeId = employee.EmployeeId,
                    EmployeeName = employee.EmployeeName,
                    WorkWarehouseId = employee.WWarehouseId,
                    EmployeePositionId = employeePosition
                };

                return new OkObjectResult(userInfo);
            }

            return new NotFoundResult();
        }
    }
}
