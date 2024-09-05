using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using WebVanChuyen.Api.Data;
using WebVanChuyen.Api.Logic;
using WebVanChuyen.Models.Models;

namespace WebVanChuyen.Api.Repositorys
{
    public class RoutePathRepository : IRoutePathRepository
    {
        protected readonly DataContextWeb appDbContext;

        public RoutePathRepository(DataContextWeb appDbContext) { this.appDbContext = appDbContext; }


        // WHRoute
        public async Task<bool> CheckWarehouses(string start, string end)
        {
            var startWH = await appDbContext.Warehouse.AsQueryable().FirstOrDefaultAsync(w => w.WarehouseId == start);
            var endWH = await appDbContext.Warehouse.AsQueryable().FirstOrDefaultAsync(w => w.WarehouseId == end);

            if (startWH == null || endWH == null) return false;
            else return true;
        }

        public async Task<ActionResult<WHRoute>> CreateRoute(StartAndEnd snE)
        {
            var check = await CheckWarehouses(snE.StartPoint, snE.EndPoint);
            if (check == false) return new BadRequestObjectResult("Error: One or both warehouses not found!");

            // Create new if not exist
            var createNew = new SearchRoute(appDbContext, snE.StartPoint, snE.EndPoint);
            await createNew.CreateListNodes();
            createNew.SearchRoutePath();
            createNew.BuildListNode();

            if (createNew.EndNode.MinCost != null)
            {
                var newRoute = new WHRoute();

                // Get attribute value
                newRoute.StartPoint = snE.StartPoint;
                newRoute.EndPoint = snE.EndPoint;
                newRoute.StopPointsList = createNew.WarehouseIds;
                newRoute.TotalCost = Math.Round(Math.Floor((double)createNew.EndNode.MinCost * 100d) / 100d, 2);
                newRoute.TotalShipCost = createNew.EndNode.ShipCost;

                var result = await appDbContext.WHRoute.AddAsync(newRoute);
                await appDbContext.SaveChangesAsync();

                return new CreatedResult(string.Empty, result.Entity);
            }

            return new NotFoundResult();
        }

        public async Task<ActionResult<IEnumerable<WHRoute>>> CreateRoutesByWarehouse(StartAndEnd snE)
        {
            var startWarehouse = await appDbContext.Warehouse.AsQueryable().FirstOrDefaultAsync(wh => wh.WarehouseId == snE.StartPoint);
            if (startWarehouse == null) return new BadRequestResult();

            var warehouses = await appDbContext.Warehouse.AsQueryable().ToListAsync();

            // Get index of start warehouse
            var index = warehouses.FindIndex(wh => wh == startWarehouse);
            // Remove start warehouse and arrange items in list warehouses 
            warehouses.Remove(startWarehouse);
            
            // Set parameter for search
            var startWarehouseId = startWarehouse.WarehouseId;
            var endWarehouseId = warehouses.Last().WarehouseId;

            // Start search route for warehouse - that return the list alike object of warehouse with assign value for route
            var createNew = new SearchRoute(appDbContext, startWarehouseId, endWarehouseId);
            await createNew.CreateListNodes();      // Create list nodes assign for process
            createNew.SearchRoutePath();            // Process assign attributes for list nodes

            // Create list on route for start warehouse
            List<WHRoute> routesList = new List<WHRoute>();
            var startNode = createNew.ListNodes.First(node => node.NodeId == snE.StartPoint);
            foreach (var warehouse in warehouses)
            {
                // Check route is exist in database else add create new route
                var existRoute = await appDbContext.WHRoute.AsQueryable().Where(r =>
                    (r.StartPoint == snE.StartPoint && r.EndPoint == warehouse.WarehouseId) ||
                    (r.StartPoint == warehouse.WarehouseId && r.EndPoint == snE.StartPoint))
                    .OrderBy(r => r.TotalCost).FirstOrDefaultAsync();

                if (existRoute != null) routesList.Add(existRoute);
                else
                {
                    var endNode = createNew.ListNodes.First(node => node.NodeId == warehouse.WarehouseId);
                    if (endNode.MinCost != null)
                    {
                        var warehouseIds = createNew.BuildListNode(endNode);

                        var newRoute = new WHRoute();

                        // Get attribute value
                        newRoute.StartPoint = startWarehouseId;
                        newRoute.EndPoint = warehouse.WarehouseId;
                        //newRoute.WarehouseIds = warehouseIds;
                        newRoute.StopPointsList = warehouseIds;
                        //value = Math.Round(Math.Floor(value*100d)/100d, 2);
                        newRoute.TotalCost = Math.Round(Math.Floor((double)endNode.MinCost*100d)/100d, 2);
                        newRoute.TotalShipCost = endNode.ShipCost;

                        var result = await appDbContext.WHRoute.AddAsync(newRoute);
                        await appDbContext.SaveChangesAsync();

                        //var addRoute = await appDbContext.WHRoute.AsQueryable().FirstOrDefaultAsync(route => route.RouteId == route.RouteId);
                        routesList.Add(result.Entity);
                    }
                }
            }

            return new CreatedResult(string.Empty, routesList);
        }

        public async Task<ActionResult<WHRoute>> GetRoute(int routeId)
        {
            var result = await appDbContext.WHRoute.AsQueryable().FirstOrDefaultAsync(r => r.RouteId == routeId);
            if (result != null) return new OkObjectResult(result);

            return new NotFoundResult();
        }

        public async Task<ActionResult<WHRoute>> GetRoute(string start, string end)
        {
            var check = await CheckWarehouses(start, end);
            if (check == false) return new BadRequestResult();

            var result = await appDbContext.WHRoute.AsQueryable().Where(
                r => (r.StartPoint == start && r.EndPoint == end) ||
                (r.StartPoint == end && r.EndPoint == start))
                .OrderBy(r => r.TotalCost).FirstOrDefaultAsync();
            if (result != null) { return new OkObjectResult(result); }

            return new NotFoundResult();
        }

        public async Task<ActionResult<IEnumerable<WHRoute>>> GetRoutes()
        {
            var result = await appDbContext.WHRoute.AsQueryable().ToListAsync();

            return new OkObjectResult(result);
        }


        // WHPath
        public async Task<ActionResult<WHPath>> CreatePath(WHPath path)
        {
            if (path == null) return new BadRequestResult();

            var result = await appDbContext.WHPath.AddAsync(path);
            await appDbContext.SaveChangesAsync();

            return new CreatedResult(string.Empty, result.Entity);
        }

        public async Task<ActionResult<IEnumerable<WHPath>>> GetPaths()
        {
            var result = await appDbContext.WHPath.AsQueryable().ToListAsync();

            return new OkObjectResult(result);
        }

        public async Task<ActionResult<WHPath>> GetPath(int pathId)
        {
            var result = await appDbContext.WHPath.AsQueryable().FirstOrDefaultAsync(p => p.PathId == pathId);
            if (result != null) return new OkObjectResult(result);

            return new NotFoundResult();
        }


        // Test
        public async Task<ActionResult<List<Node>>> GetListNodes(string startNode, string endNode)
        {
            var check = await CheckWarehouses(startNode, endNode);
            if (check == false) return new BadRequestObjectResult("Error: One or both warehouses not found!");

            var createNew = new SearchRoute(appDbContext, startNode, endNode);
            await createNew.CreateListNodes();
            createNew.SearchRoutePath();
            //createNew.BuildListNode();

            return new CreatedResult(string.Empty ,createNew.ListNodes);
        }

        public async Task<ActionResult<string>> GetListStopWH(string startNode, string endNode)
        {
            var check = await CheckWarehouses(startNode, endNode);
            if (check == false) return new BadRequestObjectResult("Error: One or both warehouses not found!");

            var createNew = new SearchRoute(appDbContext, startNode, endNode);
            await createNew.CreateListNodes();
            createNew.SearchRoutePath();
            createNew.BuildListNode();

            return new CreatedResult(string.Empty, createNew.WarehouseIds);
        }
    }
}
