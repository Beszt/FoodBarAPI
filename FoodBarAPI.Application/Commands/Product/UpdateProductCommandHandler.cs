using AutoMapper;
using MediatR;
using FoodBarAPI.Domain.Entities;
using FoodBarAPI.Domain.Interfaces;
namespace FoodBarAPI.Application.Commands;

public class UpdateProductCommandHandler(IProductRepository _productRepository, IMapper _mapper) : IRequestHandler<UpdateProductCommand>
{
    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request);

        await _productRepository.Update(product);

        return Unit.Value;
    }

}