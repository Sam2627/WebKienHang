using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using WebVanChuyen.Models.Models;
using WebVanChuyen.Web.Services;

namespace WebVanChuyen.Web.Pages.ProductPages
{
    public class ProductListBase : ComponentBase
    {
        [Inject] private IProductService ProductService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        protected IEnumerable<Product> Products { get; set; }


        protected override async Task OnInitializedAsync()
        {
            Products = (await ProductService.GetProducts()).ToList();
        }
    }
}
