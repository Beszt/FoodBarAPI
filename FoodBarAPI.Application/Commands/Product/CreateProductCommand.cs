using MediatR;
using FoodBarAPI.Application.Dtos;

namespace FoodBarAPI.Application.Commands;

public class CreateProductCommand() : ProductDto, IRequest
{
    public int UserId {get; set; }
}