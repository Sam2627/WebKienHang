using Microsoft.AspNetCore.Mvc;
using WebVanChuyen.Api.Repositorys;
using WebVanChuyen.Models.Models;

namespace WebVanChuyen.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRespository _res;

        public ProductController(IProductRespository produceRespository) { _res = produceRespository; }

        // Product
        [HttpPost("product-add")]
        public async Task<ActionResult<Product>> CreateProduct(Product newProduct)
        {
            try { return await _res.CreateProduct(newProduct); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new data record"); }
        }

        [HttpGet("product-all")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            try { return await _res.GetAllProducts(); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("product/{productId}")]
        public async Task<ActionResult<Product>> GetProductsById(int productId)
        {
            try { return await _res.GetProductsById(productId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpPut("product-update")]
        public async Task<ActionResult<Product>> UpdateProduct(Product updateProduct)
        {
            try { return await _res.UpdateProduct(updateProduct); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data"); }
        }

        [HttpDelete("product-del:{productId}")]
        public async Task<ActionResult<Product>> DeleteProduct(int productId)
        {
            try { return await _res.DeleteProduct(productId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data"); }
        }


        // Category
        [HttpPost("category-add")]
        public async Task<ActionResult<Category>> CreateCategory(Category newCategory)
        {
            try { return await _res.CreateCategory(newCategory); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new data record"); }
        }

        [HttpGet("category-all")]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            try { return await _res.GetAllCategories(); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<Category>> GetCategoryById(int categoryId)
        {
            try { return await _res.GetCategoryById(categoryId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpPut("category-update")]
        public async Task<ActionResult<Category>> UpdateCategory(Category updateCategory)
        {
            try { return await _res.UpdateCategory(updateCategory); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data"); }
        }

        [HttpDelete("category-delete/{categoryId}")]
        public async Task<ActionResult<Category>> DeleteCategory(int categoryId)
        {
            try { return await _res.DeleteCategory(categoryId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data"); }
        }
        

        // ProductCategories
        [HttpPost("productcategory-add")]
        public async Task<ActionResult<ProductCategory>> AddProductCategory(ProductCategory newProductCategories)
        {
            try { return await _res.AddProductCategory(newProductCategories); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new data record"); }
        }

        [HttpGet("productcategory-all")]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetAllProductCategories()
        {
            try { return await _res.GetAllProductCategories(); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("productcategory/{productCategoriesId}")]
        public async Task<ActionResult<ProductCategory>> GetProductsCategoryById(int productCategoriesId)
        {
            try { return await _res.GetProductsCategoryById(productCategoriesId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpGet("productcategory/product:{productId}")]
        public async Task<ActionResult<IQueryable<ProductCategory>>> GetProductCategoriesOfProduct(int productId)
        {
            try { return await _res.GetProductCategoriesOfProduct(productId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
        }

        [HttpDelete("productcategory-delete/{productCategoryId}")]
        public async Task<ActionResult<ProductCategory>> DeleteProductCategories(int productCategoryId)
        {
            try { return await _res.DeleteProductCategory(productCategoryId); }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data"); }
        }
    }
}



