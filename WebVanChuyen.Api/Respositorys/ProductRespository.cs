using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVanChuyen.Api.Data;
using WebVanChuyen.Models.Models;

namespace WebVanChuyen.Api.Repositorys
{
    public class ProductRespository : IProductRespository
    {
        private readonly DataContextWeb appDbContext;

        public ProductRespository(DataContextWeb appDbContext) { this.appDbContext = appDbContext; }

        // Product
        public async Task<ActionResult<Product>> CreateProduct(Product newProduct)
        {
            if (newProduct == null) return new BadRequestResult();

            var result = await appDbContext.Product.AddAsync(newProduct);
            await appDbContext.SaveChangesAsync();

            return new CreatedResult(string.Empty, result.Entity);
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var result = await appDbContext.Product.AsQueryable().ToListAsync();
            
            return new OkObjectResult(result);
        }

        public async Task<ActionResult<Product>> GetProductsById(int ProductId)
        {   
            var result = await appDbContext.Product.FirstOrDefaultAsync(p => p.ProductId == ProductId);
            if (result != null) return new OkObjectResult(result);

            return new NotFoundObjectResult($"Not found product with ID: {ProductId}");
        }

        public async Task<ActionResult<Product>> UpdateProduct(Product updateProduct)
        {
            var result = await appDbContext.Product.FirstOrDefaultAsync(p => p.ProductId == updateProduct.ProductId);
            if (result != null)
            {
                result.ProductName = updateProduct.ProductName;
                result.ProductDesc = updateProduct.ProductDesc;

                await appDbContext.SaveChangesAsync();

                return new OkObjectResult(result);
            }

            return new BadRequestObjectResult($"Not found product with ID: {updateProduct.ProductId}");
        }

        public async Task<ActionResult<Product>> DeleteProduct(int ProductId)
        {
            var result = await appDbContext.Product.FirstOrDefaultAsync(p => p.ProductId == ProductId);
            if (result != null)
            {
                // Remove ProductCategories of Product
                var querry = appDbContext.ProductCategory.AsQueryable();
                querry = querry.Where(q => q.Product == ProductId);
                appDbContext.ProductCategory.RemoveRange(querry);
                await appDbContext.SaveChangesAsync();

                // Remove Product
                appDbContext.Product.Remove(result);
                await appDbContext.SaveChangesAsync();

                return new OkObjectResult(result);
            }

            return new BadRequestObjectResult($"Product ID: {ProductId} not found");
        }


        // Category
        public async Task<ActionResult<Category>> CreateCategory(Category newCategory)
        {
            if (newCategory == null) return new BadRequestResult();

            var result = await appDbContext.Category.AddAsync(newCategory);
            await appDbContext.SaveChangesAsync();

            return new CreatedResult(string.Empty, result.Entity);
        }

        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            var result = await appDbContext.Category.AsQueryable().ToListAsync();
            
            return new OkObjectResult(result);
        }
        
        public async Task<ActionResult<Category>> GetCategoryById(int categoryId)
        {
            var result = await appDbContext.Category.FirstOrDefaultAsync(c => c.CategoryId == categoryId);
            if (result != null) return new OkObjectResult(result);

            return new NotFoundObjectResult($"Not found category with ID: {categoryId}");
        }

        public async Task<ActionResult<Category>> UpdateCategory(Category updateCategory)
        {
            var result = await appDbContext.Category.FirstOrDefaultAsync(c => c.CategoryId == updateCategory.CategoryId);
            if (result != null)
            {
                result.CategoryName = updateCategory.CategoryName;
                
                await appDbContext.SaveChangesAsync();

                return new OkObjectResult(result);
            }

            return new BadRequestResult();
        }

        public async Task<ActionResult<Category>> DeleteCategory(int categoryId)
        {
            var result = await appDbContext.Category.FirstOrDefaultAsync(c => c.CategoryId == categoryId);
            if (result != null)
            {
                appDbContext.Category.Remove(result);
                await appDbContext.SaveChangesAsync();

                return new OkObjectResult(result);
            }

            return new NotFoundObjectResult($"Not found category with ID: {categoryId}");
        }


        // ProductCategories
        public async Task<ActionResult<ProductCategory>> AddProductCategory(ProductCategory newProductCategory)
        {
            if (newProductCategory == null) return new BadRequestResult();

            await appDbContext.ProductCategory.AddAsync(newProductCategory);
            await appDbContext.SaveChangesAsync();

            return new CreatedResult(string.Empty, newProductCategory);
        }

        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetAllProductCategories()
        {
            var result = await appDbContext.ProductCategory.AsQueryable().ToListAsync();

            return new OkObjectResult(result);
        }

        public async Task<ActionResult<ProductCategory>> GetProductsCategoryById(int productCategoryId)
        {
            var result = await appDbContext.ProductCategory.FirstOrDefaultAsync(c => c.ProductCateId == productCategoryId);
            if (result != null) return new OkObjectResult(result);

            return new NotFoundResult();
        }

        public async Task<ActionResult<IQueryable<ProductCategory>>> GetProductCategoriesOfProduct(int productId)
        {
            var result = await appDbContext.ProductCategory.Where(p => p.Product == productId).AsQueryable().ToListAsync();
            
            return new OkObjectResult(result);
        }

        public async Task<ActionResult<ProductCategory>> DeleteProductCategory(int productCategoryId)
        {
            var result = await appDbContext.ProductCategory.FirstOrDefaultAsync(p => p.ProductCateId == productCategoryId);
            if(result != null)
            {
                appDbContext.ProductCategory.Remove(result);
                await appDbContext.SaveChangesAsync();

                return new OkObjectResult(result);
            }

            return new NotFoundResult();
        }
    }
}
