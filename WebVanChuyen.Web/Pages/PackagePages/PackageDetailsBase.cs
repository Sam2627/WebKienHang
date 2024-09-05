using Microsoft.AspNetCore.Components;
using WebVanChuyen.Models.Models;
using WebVanChuyen.Web.Services;

namespace WebVanChuyen.Web.Pages.PackagePages
{
    public class PackageDetailsBase : ComponentBase
    {
        [Inject] private IWarehouseService WarehouseService { get; set; }
        [Inject] private IEmployeeService EmployeeService { get; set; }
        [Inject] private IPackageService PackageService { get; set; }

        [Parameter] public int Id { get; set; }

        protected Warehouse Warehouse { get; set; }
        protected List<Employee> Employee { get; set; }
        protected Package Package { get; set; } = new Package();


        protected override async Task OnInitializedAsync()
        {
            if (Id != null)
            {
                Package = await PackageService.GetPackage(Id);
                Warehouse = await WarehouseService.GetWarehouse(Package.LocaId);
                Employee = (await EmployeeService.GetEmployeesByPackage(Id)).ToList();
            }
        }
    }
}
