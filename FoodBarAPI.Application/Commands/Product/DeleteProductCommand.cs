using MediatR;
using FoodBarAPI.Application.Dtos;

namespace FoodBarAPI.Application.Commands;

public class DeleteProductCommand(long _barcode) : IRequest
{
    public long Barcode {get; set; } = _barcode;
}