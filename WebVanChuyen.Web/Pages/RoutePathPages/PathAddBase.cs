using Microsoft.AspNetCore.Components;
using WebVanChuyen.Models.Models;
using WebVanChuyen.Web.Services;

namespace WebVanChuyen.Web.Pages.RoutePathPages
{
    public class PathAddBase : ComponentBase
    {
        [Inject] private IWarehouseService WarehouseService { get; set; }
        [Inject] private IRoutePathService WHRouteService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        protected List<Warehouse> Warehouses { get; set; }
        protected List<Warehouse> Starts { get; set; } = new List<Warehouse>();
        protected List<Warehouse> Ends { get; set; } = new List<Warehouse>();

        public string RouteExist { get; set; }
        protected bool ReloadStart { get; set; } = true;
        protected bool ReloadEnd { get; set; } = true;
        protected WHPath Path { get; set; } = new WHPath();


        protected override async Task OnInitializedAsync()
        {
            Warehouses = await WarehouseService.GetWarehouses();
        }

        protected async Task HandleValidSubmit()
        {
            var route = await WHRouteService.GetPath(Path.StartPoint, Path.EndPoint);
            if (route == null)
            {
                await WHRouteService.CreatePath(Path);

                NavigationManager.NavigateTo("/paths", true);
            }
            else
            {
                RouteExist = "Đã có đường đi";
            }
        }

        protected void OnclickStart()
        {
            if (ReloadStart)
            {
                Starts.Clear();

                Starts = Warehouses.Where(wh => wh.WarehouseId != Path.EndPoint).ToList();
                ReloadStart = false;

                StateHasChanged();
            }
        }

        protected void OnSelectStart()
        {
            if (Path.StartPoint == Path.EndPoint)
            {
                Ends.Clear();
                ReloadEnd = true;

                StateHasChanged();
            }
        }

        protected void OnclickEnd()
        {
            if (ReloadEnd)
            {
                Ends.Clear();

                Ends = Warehouses.Where(wh => wh.WarehouseId != Path.StartPoint).ToList();
                ReloadEnd = false;

                StateHasChanged();
            }
        }

        protected void OnSelectEnd()
        {
            if (Path.EndPoint == Path.StartPoint)
            {
                Starts.Clear();
                ReloadStart = true;

                StateHasChanged();
            }
        }
    }
}
