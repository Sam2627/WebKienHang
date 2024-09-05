using WebVanChuyen.Models.Models;

namespace WebVanChuyen.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        public ProductService(HttpClient httpClient) { _httpClient = httpClient; }


        // Product
        public async Task CreateProduct(Product product)
        {
            await _httpClient.PostAsJsonAsync("api/product-add", product);
        }

        public async Task<Product> GetProduct(int productId)
        {
            return await _httpClient.GetFromJsonAsync<Product>($"api/product/{productId}");
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _httpClient.GetFromJsonAsync<List<Product>>("api/product-all");
        }

        public async Task UpdateProduct(Product updateProduct)
        {
            await _httpClient.PutAsJsonAsync("api/product-update", updateProduct);
        }

        public async Task DeleteProduct(int productId)
        {
            await _httpClient.DeleteAsync($"api/product-delete:{productId}");
        }


        // Category
        public async Task AddCategory(Category Category)
        {
            await _httpClient.PostAsJsonAsync("api/category-add", Category);
        }

        public async Task<Category> GetCategory(int categoryId)
        {
            return await _httpClient.GetFromJsonAsync<Category>($"api/category/{categoryId}");
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _httpClient.GetFromJsonAsync<List<Category>>("api/category-all");
        }

        public async Task UpdateCategory(Category updateCategory)
        {
            await _httpClient.PutAsJsonAsync("api/category-update", updateCategory);
        }

        public async Task DeleteCategory(int categoryId)
        {
            await _httpClient.DeleteAsync($"api/products/category-delete:{categoryId}");
        }
 

        // ProductCategories
        public async Task AddProductCategory(ProductCategory newProductCategory)
        {
            await _httpClient.PostAsJsonAsync("api/productcategory-add", newProductCategory);
        }

        public async Task<ProductCategory> GetProductCategory(int productCategoryId)
        {
            return await _httpClient.GetFromJsonAsync<ProductCategory>($"api/productcategory/{productCategoryId}");
        }

        public async Task<List<ProductCategory>> GetProductCategories()
        {
            return await _httpClient.GetFromJsonAsync<List<ProductCategory>>("api/productcategory-all");
        }

        public async Task<List<ProductCategory>> GetProductCategoriesByProduct(int productId)
        {
            return await _httpClient.GetFromJsonAsync<List<ProductCategory>>($"api/productcategory/product:{productId}");
        }

        public async Task DeleteProductCategory(int productCategoryId)
        {
            await _httpClient.DeleteAsync($"api/products/productcategory-delete:{productCategoryId}");
        }
    }
}
