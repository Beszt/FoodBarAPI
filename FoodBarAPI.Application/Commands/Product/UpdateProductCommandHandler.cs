using AutoMapper;
using MediatR;
using FoodBarAPI.Domain.Entities;
using FoodBarAPI.Domain.Interfaces;

namespace FoodBarAPI.Application.Commands;

public class UpdateProductCommandHandler(IFoodBarRepository _FoodBarRepository, IMapper _mapper) : IRequestHandler<UpdateProductCommand>
{
    async Task IRequestHandler<UpdateProductCommand>.Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request);

        await _FoodBarRepository.Update(product);
    }
}