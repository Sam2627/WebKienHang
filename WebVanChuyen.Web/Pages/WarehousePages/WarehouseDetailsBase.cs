using Microsoft.AspNetCore.Components;
using WebVanChuyen.Models.Models;
using WebVanChuyen.Web.Services;

namespace WebVanChuyen.Web.Pages.WarehousePages
{
    public class WarehouseDetailsBase : ComponentBase
    {
        [Inject] private IWarehouseService WarehouseService { get; set; }

        [Parameter] public string Id { get; set; }
        protected Warehouse Warehouse { get; set; } = new Warehouse();


        protected override async Task OnInitializedAsync()
        {
            if (Id != null)
            {
                Warehouse = await WarehouseService.GetWarehouse(Id);
            }
        }
    }
}
