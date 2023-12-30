using MediatR;
using FoodBarAPI.Application.Dtos;

namespace FoodBarAPI.Application.Commands;

public class DeleteProductCommand : ProductDto, IRequest
{
    public DeleteProductCommand(long barcode)
    {
        Barcode = barcode;
    }
}