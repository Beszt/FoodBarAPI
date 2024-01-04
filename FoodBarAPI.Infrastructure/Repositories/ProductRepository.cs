using Microsoft.EntityFrameworkCore;
using FoodBarAPI.Domain.Entities;
using FoodBarAPI.Domain.Interfaces;
using FoodBarAPI.Infrastructure.Persistence;

namespace FoodBarAPI.Infrastructure.Repositories;

public class ProductRepository(FoodBarDbContext _dbContext) : IProductRepository
{
    public async Task Create(Product product)
    {
        _dbContext.Add(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Product?> Get(long barcode)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Barcode == barcode);
        if (product != null)
        {
            var productDetails = await _dbContext.ProductsDetails.FirstOrDefaultAsync(pd => pd.ProductId == product.Id);
            if (productDetails != null)
                product.ProductDetails = productDetails;
        }

        return product;
    }

    public async Task Update(Product product)
    {
        var prod = _dbContext.Products.FirstOrDefault(p => p.Barcode == product.Barcode);

        if (prod != null)
        {
            prod.Name = product.Name;
            prod.Description = product.Description;
            prod.Image = product.Image;
            prod.UpdatedAt = DateTime.UtcNow;
            prod.UpdatedBy = product.UpdatedBy;

            var prodDet = _dbContext.ProductsDetails.FirstOrDefault(pd => pd.ProductId == prod.Id);
            if (prodDet != null)
                prod.ProductDetails = prodDet;
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(long barcode)
    {
        var product = _dbContext.Products.FirstOrDefault(p => p.Barcode == barcode);

        if (product != null)
            _dbContext.Remove(product);

        await _dbContext.SaveChangesAsync();
    }

    public bool Exists(long barcode) =>
        _dbContext.Products.FirstOrDefault(p => p.Barcode == barcode) != null;

    public bool WasCreatedBy(long barcode, int userId)
    {
        var product = _dbContext.Products.FirstOrDefault(p => p.Barcode == barcode);

        if (product != null)
            return product.CreatedBy == userId;
        else
            return false;
    }
}