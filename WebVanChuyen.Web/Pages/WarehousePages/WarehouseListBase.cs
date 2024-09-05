using Microsoft.AspNetCore.Components;
using WebVanChuyen.Models.Models;
using WebVanChuyen.Web.Services;

namespace WebVanChuyen.Web.Pages.WarehousePages
{
    public class WarehouseListBase : ComponentBase
    {
        [Inject] private IWarehouseService WarehouseService { get; set; }

        protected List<Warehouse> Warehouses { get; set; } = new List<Warehouse>();


        protected override async Task OnInitializedAsync()
        {
            Warehouses = await WarehouseService.GetWarehouses();
        }
    }
}
