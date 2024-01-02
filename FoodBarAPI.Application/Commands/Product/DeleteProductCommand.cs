using MediatR;

namespace FoodBarAPI.Application.Commands;

public class DeleteProductCommand() : IRequest
{
    public long Barcode {get; set; }
    public int UserId {get; set; }
}