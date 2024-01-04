using MediatR;
using FoodBarAPI.Application.Dtos;

namespace FoodBarAPI.Application.Commands;

public class CreateUserCommand : UserDto, IRequest
{
    
}