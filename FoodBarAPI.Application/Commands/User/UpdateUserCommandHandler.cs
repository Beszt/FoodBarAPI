using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MediatR;
using FoodBarAPI.Domain.Interfaces;
using FoodBarAPI.Domain.Entities;

namespace FoodBarAPI.Application.Commands;

public class UpdateUserCommandHandler(IUserRepository _userRepository, IMapper _mapper) : IRequestHandler<UpdateUserCommand>
{
    async Task IRequestHandler<UpdateUserCommand>.Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);

        user.RoleId = _userRepository.GetRoleIdByRoleName(request.Role);

        var passwordHasher = new PasswordHasher<User>();
        user.Password = passwordHasher.HashPassword(user, request.Password);

        await _userRepository.Update(user);
    }
}