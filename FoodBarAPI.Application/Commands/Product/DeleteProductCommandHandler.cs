using MediatR;
using FoodBarAPI.Domain.Interfaces;

namespace FoodBarAPI.Application.Commands;

public class DeleteProductCommandHandler(IFoodBarRepository _FoodBarRepository) : IRequestHandler<DeleteProductCommand>
{
    async Task IRequestHandler<DeleteProductCommand>.Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await _FoodBarRepository.Delete(request.Barcode);
    }
}