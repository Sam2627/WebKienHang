using Microsoft.AspNetCore.Components;
using WebVanChuyen.Models.Models;
using WebVanChuyen.Web.Services;

namespace WebVanChuyen.Web.Pages.RoutePathPages
{
    public class RouteAddBase : ComponentBase
    {
        [Inject] private IWarehouseService WarehouseService { get; set; }
        [Inject] private IRoutePathService RoutePathService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Parameter] public EventCallback<List<WHRoute>> ReloadRoutes { get; set; }

        protected List<Warehouse> Warehouses { get; set; }
        protected List<Warehouse> Starts { get; set; } = new List<Warehouse>();
        protected List<Warehouse> Ends { get; set; } = new List<Warehouse>();

        public string ErrorMessage { get; set; }
        protected bool ReloadStart { get; set; } = true;
        protected bool ReloadEnd { get; set; } = true;
        protected StartAndEnd SnE { get; set; } = new StartAndEnd();

        protected override async Task OnInitializedAsync()
        {
            Warehouses = await WarehouseService.GetWarehouses();
            Starts = Warehouses;
            Ends = Warehouses;
        }

        protected async Task HandleValidSubmit()
        {
            var route = await RoutePathService.GetRoute(SnE.StartPoint, SnE.EndPoint);
            if (route == null)
            {
                await RoutePathService.CreateRoute(SnE);

                var newRoutes = (await RoutePathService.GetRoutes()).ToList();

                // Call parent component redender
                ReloadRoutes.InvokeAsync(newRoutes);
                //NavigationManager.NavigateTo("/routes", true);

                ResetSelect();
            }
            else
            {
                ErrorMessage = "Đã có tuyến đường";
            }
        }

        protected void OnSelectStart()
        {
            Ends = Warehouses.Where(wh => wh.WarehouseId != SnE.StartPoint).ToList();
        }

        protected void OnSelectEnd()
        {
            Starts = Warehouses.Where(wh => wh.WarehouseId != SnE.EndPoint).ToList();
        }

        protected async Task OnClickButton()
        {
            if (SnE.StartPoint == "" || SnE.StartPoint == null)
            {
                ErrorMessage = "Xin chọn điểm khởi hành";
            }
            else
            {
                SnE.EndPoint = "string";   // put value to valid object

                await RoutePathService.CreateRoutesByWarehouse(SnE);

                var newRoutes = (await RoutePathService.GetRoutes()).ToList();

                // Call parent component redender
                ReloadRoutes.InvokeAsync(newRoutes);
                //NavigationManager.NavigateTo("/routes", true);

                ResetSelect();
            }
        }

        protected void ResetSelect()
        {
            SnE.StartPoint = "";
            SnE.EndPoint = "";

            StateHasChanged();
        }
    }
}
