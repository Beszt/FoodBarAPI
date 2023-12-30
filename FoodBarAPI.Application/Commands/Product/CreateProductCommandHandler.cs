using AutoMapper;
using MediatR;
using FoodBarAPI.Domain.Entities;
using FoodBarAPI.Domain.Interfaces;

namespace FoodBarAPI.Application.Commands;

public class CreateProductCommandHandler(IProductRepository _productRepository, IMapper _mapper) : IRequestHandler<CreateProductCommand>
{
    async Task IRequestHandler<CreateProductCommand>.Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request);

        await _productRepository.Create(product);
    }
}