using MediatR;
using FoodBarAPI.Domain.Interfaces;
namespace FoodBarAPI.Application.Commands;

public class DeleteProductCommandHandler(IProductRepository _productRepository) : IRequestHandler<DeleteProductCommand>
{
    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await _productRepository.Delete(request.Code);

        return Unit.Value;
    }

}