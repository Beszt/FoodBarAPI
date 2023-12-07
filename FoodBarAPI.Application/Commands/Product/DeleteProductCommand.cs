using MediatR;
using FoodBarAPI.Application.Dtos;

namespace FoodBarAPI.Application.Commands;

public class DeleteProductCommand(long code) : ProductDto, IRequest
{
    public long Code { get; set; } = code;
}