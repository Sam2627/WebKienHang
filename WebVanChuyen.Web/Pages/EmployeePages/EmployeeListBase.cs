using AutoMapper;
using Microsoft.AspNetCore.Components;
using WebVanChuyen.Models.Models;
using WebVanChuyen.Web.Services;

namespace WebVanChuyen.Web.Pages
{
    public class EmployeeListBase : ComponentBase
    {
        [Inject] private IEmployeeService EmployeeService { get; set; }
        [Inject] private IWarehouseService WarehouseService { get; set; }
        [Inject] private IMapper Mapper { get; set; }

        protected List<Employee> Employees { get; set; } = new List<Employee>();
        protected List<EmployeePosition> EmployeesPositions { get; set; } = new List<EmployeePosition>();
        protected List<Warehouse> Warehouses { get; set; } = new List<Warehouse>();


        protected override async Task OnInitializedAsync()
        {
            Employees = (await EmployeeService.GetEmployees()).ToList();
            EmployeesPositions = (await EmployeeService.GetEmployeePositions()).ToList();
            Warehouses = await WarehouseService.GetWarehouses();
        }
    }
}
