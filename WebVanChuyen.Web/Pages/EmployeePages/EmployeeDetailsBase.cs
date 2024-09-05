using Microsoft.AspNetCore.Components;
using WebVanChuyen.Models.Models;
using WebVanChuyen.Web.Services;

namespace WebVanChuyen.Web.Pages
{
    public class EmployeeDetailsBase : ComponentBase
    {
        [Inject] private IEmployeeService EmployeeService { get; set; }

        [Inject] private IWarehouseService WarehouseService { get; set; }

        [Inject] private NavigationManager NavigationManager { get; set; }


        [Parameter] public string Id { get; set; }

        protected bool IsCreate { get; set; }
        protected string PageHeader { get; set; }

        protected Employee Employee { get; set; } = new Employee();
        protected List<EmployeePosition> EmployeePositions { get; set; } = new List<EmployeePosition>();
        protected List<Warehouse> Warehouses { get; set; } = new List<Warehouse>();


        protected override async Task OnInitializedAsync()
        {
            if (Id == null) 
            {
                PageHeader = "Thông tin nhân viên";
                IsCreate = false;
                Employee = await EmployeeService.GetEmployee(int.Parse(Id));
            }
            else
            {
                PageHeader = "Tạo nhân viên";
                IsCreate = true;
                EmployeePositions = (await EmployeeService.GetEmployeePositions()).ToList();
                Warehouses = await WarehouseService.GetWarehouses();
            }
        }

        protected async Task Created_Click()
        {
            Employee.LocaId = Employee.WWarehouseId;

            await EmployeeService.CreateEmployee(Employee);

            NavigationManager.NavigateTo($"/employees");
        }
    }
}
