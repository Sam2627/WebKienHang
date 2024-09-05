using Microsoft.AspNetCore.Mvc;
using WebVanChuyen.Api.Repositorys;
using WebVanChuyen.Models.Models;

namespace WebVanChuyen.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class RoutePathController : ControllerBase
    {
        private readonly IRoutePathRepository _res;

        public RoutePathController(IRoutePathRepository routePathRepository) { _res = routePathRepository; }


        // WHRoute
        [HttpPost("route:add")]
        public async Task<ActionResult<WHRoute>> CreateWHRoute(StartAndEnd snE)
        {
            try { return await _res.CreateRoute(snE); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new data record"); }
        }

        [HttpPost("route:add-list")]
        public async Task<ActionResult<IEnumerable<WHRoute>>> CreateRoutesByWarehouse(StartAndEnd snE)
        {
            try { return await _res.CreateRoutesByWarehouse(snE); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new data record"); }
        }

        [HttpGet("route/{wHRouteId}")]
        public async Task<ActionResult<WHRoute>> GetRoute(int wHRouteId)
        {
            try { return await _res.GetRoute(wHRouteId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("route/warehouse:{start}@{end}")]
        public async Task<ActionResult<WHRoute>> GetRoute(string start, string end)
        {
            try { return await _res.GetRoute(start, end); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("route:all")]
        public async Task<ActionResult<IEnumerable<WHRoute>>> GetRoutes()
        {
            try { return await _res.GetRoutes(); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }


        // WHPath
        [HttpPost("path:add")]
        public async Task<ActionResult<WHPath>> CreatePath(WHPath path)
        {
            try { return await _res.CreatePath(path); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new data record"); }
        }

        [HttpGet("path/{wHPathId}")]
        public async Task<ActionResult<WHPath>> GetPath(int pathId)
        {
            try { return await _res.GetPath(pathId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("path:all")]
        public async Task<ActionResult<IEnumerable<WHPath>>> GetPaths()
        {
            try { return await _res.GetPaths(); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }


        // Test
        [HttpGet("route/test-list-nodes)")]
        public async Task<ActionResult<List<Node>>> GetListNodes(string startNode, string endNode)
        {
            try { return await _res.GetListNodes(startNode, endNode); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error getting new data record"); }
        }

        [HttpGet("route/test-list-stop-wh)")]
        public async Task<ActionResult<string>> GetListStopWH(string startNode, string endNode)
        {
            try { return await _res.GetListStopWH(startNode, endNode); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error getting new data record"); }
        }
    }
}
