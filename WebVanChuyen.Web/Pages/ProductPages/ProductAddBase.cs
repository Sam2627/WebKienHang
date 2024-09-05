using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using WebVanChuyen.Models.Models;
using WebVanChuyen.Web.Services;

namespace WebVanChuyen.Web.Pages.ProductPages
{
    [Authorize(Policy = "AdminOnly")]
    public class ProductAddBase : ComponentBase
    {
        [Inject] private IProductService ProductService { get; set; }
        //[Inject] public IMapper Mapper { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        protected Product Product { get; set; } = new Product();
        protected List<Category> Categories { get; set; } = new List<Category>();
        protected List<AddCategory> WCategories { get; set; } = new List<AddCategory>();
        protected ProductCategory ProductCategories { get; set; } = new ProductCategory();


        protected override async Task OnInitializedAsync()
        {
            Product = new Product()
            {
                ProductName = "",
                ProductDesc = ""
            };

            //Categories = (await ProductService.GetAllCategories()).ToList();

            //Mapper.Map(Categories, WCategories);
        }

        protected async Task HandleValidSubmit()
        {
            await ProductService.CreateProduct(Product);

            //var newProduct = await ProductService.GetProductByNew();

            /*
            foreach (var item in WCategories) 
            {
                if (item.IsCheck == true)
                {
                    ProductCategories.Product = newProduct.ProductId;
                    ProductCategories.Category = item.CategoryId;

                    await ProductService.AddProductCategories(ProductCategories);
                }
            }
            */
            NavigationManager.NavigateTo("/products", true);
        }
    }
}
