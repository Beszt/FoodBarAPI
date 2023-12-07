using FoodBarAPI.Application.Dtos;
using MediatR;

namespace FoodBarAPI.Application.Queries;

public class GetProductQuery(long code) : IRequest<ProductDto>
{
    public long Code { get; set; } = code;
}