using AutoMapper;
using MediatR;
using FoodBarAPI.Application.Dtos;
using FoodBarAPI.Domain.Interfaces;

namespace FoodBarAPI.Application.Queries;

public class GetUserQueryHandler(IUserRepository _userRepository, IMapper _mapper) : IRequestHandler<GetUserQuery, UserDto?>
{
    async Task<UserDto?> IRequestHandler<GetUserQuery, UserDto?>.Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Get(request.Login);
        var dto = _mapper.Map<UserDto>(user);
        
        if (dto != null)
        {
            dto.Password = "HIDDEN";
            dto.Role =  _userRepository.GetRoleName(dto.Login)!;
        }

        return dto;
    }
}