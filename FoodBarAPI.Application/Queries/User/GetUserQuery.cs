using FoodBarAPI.Application.Dtos;
using MediatR;

namespace FoodBarAPI.Application.Queries;

public class GetUserQuery(string _Login) : IRequest<UserDto?>
{
    public string Login { get; set; } = _Login;
}