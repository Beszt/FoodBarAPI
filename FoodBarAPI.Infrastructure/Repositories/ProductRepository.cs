using FoodBarAPI.Domain.Entities;
using FoodBarAPI.Domain.Interfaces;
using FoodBarAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoodBarAPI.Infrastructure.Repositories
{
    public class ProductRepository(ProductDbContext _dbContext) : IProductRepository
    {
        public async Task Create(Product product)
        {
            _dbContext.Add(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(long barcode)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.Barcode == barcode);

            if (product != null)
                _dbContext.Remove(product);

            await _dbContext.SaveChangesAsync();
        }

        public Task<Product?> Get(long barcode)
            => _dbContext.Products.FirstOrDefaultAsync(p => p.Barcode == barcode);

        public async Task Update(Product product)
        {
            var pd = _dbContext.Products.FirstOrDefault(p => p.Barcode == product.Barcode);

            if (pd != null)
            {
                pd.Name = product.Name;
                pd.Description = product.Description;
                pd.ProductDetails = product.ProductDetails;
                pd.Image = product.Image;
                pd.UpdatedAt = DateTime.UtcNow;
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}