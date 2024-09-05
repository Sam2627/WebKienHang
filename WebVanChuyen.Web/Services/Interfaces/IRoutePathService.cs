using WebVanChuyen.Models.Models;

namespace WebVanChuyen.Web.Services
{
    public interface IRoutePathService
    {
        // WHRoute
        Task<WHRoute> CreateRoute(StartAndEnd snE);
        Task CreateRoutesByWarehouse(StartAndEnd snE);
        Task<WHRoute> GetRoute(int wHRouteId);
        Task<WHRoute> GetRoute(string start, string end);
        Task<List<WHRoute>> GetRoutes();


        // WHPath
        Task CreatePath(WHPath node);
        Task<WHPath> GetPath(int wHPathId);
        Task<WHPath> GetPath(string start, string end);
        Task<List<WHPath>> GetPaths();
    }
}
