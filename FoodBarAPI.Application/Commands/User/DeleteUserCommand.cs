using MediatR;

namespace FoodBarAPI.Application.Commands;

public class DeleteUserCommand(string login) : IRequest
{
    public string Login {get; set; } = login;
}