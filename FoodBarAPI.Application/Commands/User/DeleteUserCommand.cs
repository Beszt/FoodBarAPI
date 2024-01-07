using MediatR;

namespace FoodBarAPI.Application.Commands;

public class DeleteUserCommand() : IRequest
{
    public string Login {get; set; } = default!;
}