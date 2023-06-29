using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Models
{
    public class ProductServices
    {
        private readonly AddProductDbContext  _dbContext;

        public ProductServices(AddProductDbContext  dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _dbContext.Products.OrderBy(p => p.Price).ToListAsync();
        }

        public async Task<Product> CreateProduct(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetProductById(int productId)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            return product;
        }

        public async Task<Product> UpdateProduct(int productId, Product updatedProduct)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            if (product == null)
                return null;

            product.Name = updatedProduct.Name;
          
            product.Price = updatedProduct.Price;

            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            if (product == null)
                return false;

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
