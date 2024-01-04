using MediatR;
using FoodBarAPI.Domain.Interfaces;
namespace FoodBarAPI.Application.Commands;

public class DeleteUserCommandHandler(IUserRepository _userRepository) : IRequestHandler<DeleteUserCommand>
{
    async Task IRequestHandler<DeleteUserCommand>.Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.Delete(request.Login);
    }
}