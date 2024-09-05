using Microsoft.AspNetCore.Components;
using WebVanChuyen.Models.Models;
using WebVanChuyen.Web.Services;

namespace WebVanChuyen.Web.Pages.RoutePathPages
{
    public class RouteListBase : ComponentBase
    {
        [Inject] private IRoutePathService RoutePathService { get; set; }

        protected List<WHRoute> Routes { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Routes = (await RoutePathService.GetRoutes()).ToList();
        }

        public string EditStopPoint(string start, string end, string stopString)
        {
            var newString = stopString.Replace(start + ",", string.Empty);
            newString = newString.Replace(", " + end, string.Empty);
            newString = newString.Replace(end, string.Empty);
            
            return newString;
        }

        protected void ReloadRoutes(List<WHRoute> value)
        {
            Routes = value;
        }

    }
}
