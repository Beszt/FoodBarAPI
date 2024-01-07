using MediatR;
using FoodBarAPI.Application.Dtos;

namespace FoodBarAPI.Application.Queries;

public class GetProductQuery() : IRequest<ProductDto>
{
    public long Barcode { get; set; }
}