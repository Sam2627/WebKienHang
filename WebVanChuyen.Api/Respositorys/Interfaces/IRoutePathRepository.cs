using Microsoft.AspNetCore.Mvc;
using WebVanChuyen.Models.Models;

namespace WebVanChuyen.Api.Repositorys
{
    public interface IRoutePathRepository
    {
        // WHRoute
        Task<ActionResult<WHRoute>> CreateRoute(StartAndEnd snE);
        Task<ActionResult<IEnumerable<WHRoute>>> CreateRoutesByWarehouse(StartAndEnd snE);
        Task<ActionResult<WHRoute>> GetRoute(int routeId);
        Task<ActionResult<WHRoute>> GetRoute(string start, string end);
        Task<ActionResult<IEnumerable<WHRoute>>> GetRoutes();


        // WHPath
        Task<ActionResult<WHPath>> CreatePath(WHPath path);
        Task<ActionResult<IEnumerable<WHPath>>> GetPaths();
        Task<ActionResult<WHPath>> GetPath(int pathId);


        // Test
        Task<ActionResult<List<Node>>> GetListNodes(string startNode, string endNode);
        Task<ActionResult<string>> GetListStopWH(string startNode, string endNode);
    }
}
