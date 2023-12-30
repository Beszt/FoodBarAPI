using FoodBarAPI.Application.Dtos;
using MediatR;

namespace FoodBarAPI.Application.Queries;

public class GetProductQuery(long barcode) : IRequest<ProductDto>
{
    public long Barcode { get; set; } = barcode;
}