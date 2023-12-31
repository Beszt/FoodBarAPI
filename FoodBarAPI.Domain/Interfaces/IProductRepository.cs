using FoodBarAPI.Domain.Entities;

namespace FoodBarAPI.Domain.Interfaces;

public interface IProductRepository
{
    Task Create(Product product);
    Task<Product?> Get(long barcode);
    Task Update(Product product);
    Task Delete(long barcode);

    bool Exists(long barcode);
    bool WasCreatedBy(long barcode, int userId);
}