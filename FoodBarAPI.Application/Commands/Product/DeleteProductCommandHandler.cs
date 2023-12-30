using MediatR;
using FoodBarAPI.Domain.Interfaces;
namespace FoodBarAPI.Application.Commands;

public class DeleteProductCommandHandler(IProductRepository _productRepository) : IRequestHandler<DeleteProductCommand>
{
    async Task IRequestHandler<DeleteProductCommand>.Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await _productRepository.Delete(request.Code);
    }
}