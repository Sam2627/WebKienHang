using Microsoft.AspNetCore.Mvc;
using WebVanChuyen.Models.Models;

namespace WebVanChuyen.Api.Repositorys
{
    public interface IProductRespository
    {
        // Product Table
        Task<ActionResult<Product>> CreateProduct(Product newProduct);
        Task<ActionResult<IEnumerable<Product>>> GetAllProducts();
        Task<ActionResult<Product>> GetProductsById(int productId);
        Task<ActionResult<Product>> UpdateProduct(Product updateProduct);
        Task<ActionResult<Product>> DeleteProduct(int productId);


        // Category Table
        Task<ActionResult<Category>> CreateCategory(Category newCategory);
        Task<ActionResult<IEnumerable<Category>>> GetAllCategories();
        Task<ActionResult<Category>> GetCategoryById(int categoryId);
        Task<ActionResult<Category>> UpdateCategory(Category updateCategory);
        Task<ActionResult<Category>> DeleteCategory(int categoryId);


        // ProductCategories Table
        Task<ActionResult<ProductCategory>> AddProductCategory(ProductCategory newProductCategory);
        Task<ActionResult<IEnumerable<ProductCategory>>> GetAllProductCategories();
        Task<ActionResult<ProductCategory>> GetProductsCategoryById(int productCategoryId);
        Task<ActionResult<IQueryable<ProductCategory>>> GetProductCategoriesOfProduct(int productId);
        Task<ActionResult<ProductCategory>> DeleteProductCategory(int productCategoryId);
    }
}
