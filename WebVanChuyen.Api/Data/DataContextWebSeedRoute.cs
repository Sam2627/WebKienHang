using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.Intrinsics.X86;
using WebVanChuyen.Api.Logic;
using WebVanChuyen.Api.Repositorys;
using WebVanChuyen.Models.Models;

namespace WebVanChuyen.Api.Data
{
    public static class DataContextWebSeedRoute
    {
        private static readonly DataContextWeb appDbContext;
        public static IWarehouseRepository WarehouseRepository { get; set; }
        public static IRoutePathRepository RouteRepository { get; set; }

        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            // Setting database
            var context = serviceProvider.GetService<DataContextWeb>();

            var warehousesList = await context.Warehouse.AsQueryable().ToListAsync();

            int nextIndex;

            foreach (var warehouse in warehousesList)
            {
                // Get index of item
                int index = warehousesList.FindIndex(wh => wh.WarehouseId == warehouse.WarehouseId);
                nextIndex = index++;       // Select the first next Warehouse

                // Start add route for warehouse to the last item
                do
                {
                    // Get next Warehouse in list
                    var nextWarehouse = warehousesList[nextIndex];

                    // Create new route
                    var createNew = new SearchRoute(context, warehouse.WarehouseId, nextWarehouse.WarehouseId);
                    
                    var newRoute = new WHRoute();
                    // Check route between tow points
                    if (createNew.EndNode.MinCost != null)
                    {
                        newRoute.StartPoint = warehouse.WarehouseId;    
                        newRoute.EndPoint = nextWarehouse.WarehouseId;
                        newRoute.StopPointsList = createNew.WarehouseIds;
                        newRoute.TotalCost = (double)createNew.EndNode.MinCost;
                        newRoute.TotalShipCost = createNew.EndNode.ShipCost;

                        var result = await context.WHRoute.AddAsync(newRoute);
                        await context.SaveChangesAsync();
                    }

                    // Select next visit warehouse 
                    nextIndex = nextIndex++;
                }
                while (nextIndex < warehousesList.Count);
            }
        }
    }
}
