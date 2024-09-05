using WebVanChuyen.Models.Models;

namespace WebVanChuyen.Web.Services
{
    public interface IProductService
    {
        // Product
        Task CreateProduct(Product product);
        Task<Product> GetProduct(int productId);
        Task<List<Product>> GetProducts();
        Task UpdateProduct(Product updatedProduct);
        Task DeleteProduct(int productId);


        // Category
        Task AddCategory(Category newCategory);
        Task<Category> GetCategory(int categoryId);
        Task<List<Category>> GetCategories();
        Task UpdateCategory(Category updatedProduct);
        Task DeleteCategory(int categoryId);


        // ProductCategory Table
        Task AddProductCategory(ProductCategory newProductCategory);
        Task<ProductCategory> GetProductCategory(int productCategoryId);
        Task<List<ProductCategory>> GetProductCategories();
        Task<List<ProductCategory>> GetProductCategoriesByProduct(int productId);
        Task DeleteProductCategory(int productCategoryId);
    }
}
