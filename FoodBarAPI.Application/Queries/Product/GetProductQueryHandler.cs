using AutoMapper;
using MediatR;
using FoodBarAPI.Domain.Interfaces;
using FoodBarAPI.Application.Dtos;

namespace FoodBarAPI.Application.Queries;

public class GetProductQueryHandler(IProductRepository _productRepository, IMapper _mapper) : IRequestHandler<GetProductQuery, ProductDto>
{
    public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.Get(request.Barcode);
        var dto = _mapper.Map<ProductDto>(product);
        
        return dto;
    }
}