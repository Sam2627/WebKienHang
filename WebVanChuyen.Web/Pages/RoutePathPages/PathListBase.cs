using Microsoft.AspNetCore.Components;
using WebVanChuyen.Models.Models;
using WebVanChuyen.Web.Services;

namespace WebVanChuyen.Web.Pages.RoutePathPages
{
    public class RoutePathListBase : ComponentBase
    {
        [Inject] private IWarehouseService WarehouseService { get; set; }
        [Inject] private IRoutePathService WHPathService { get; set; }

        protected List<Warehouse> Warehouses { get; set; }
        protected List<WHPath> PathList { get; set; }


        protected override async Task OnInitializedAsync()
        {
            Warehouses = await WarehouseService.GetWarehouses();
            PathList = (await WHPathService.GetPaths()).ToList();
        }
    }
}
