using Microsoft.AspNetCore.Identity;
using AutoMapper;
using MediatR;
using FoodBarAPI.Domain.Interfaces;
using FoodBarAPI.Domain.Entities;

namespace FoodBarAPI.Application.Commands;

public class CreateUserCommandHandler(IUserRepository _userRepository, IMapper _mapper) : IRequestHandler<CreateUserCommand>
{
    async Task IRequestHandler<CreateUserCommand>.Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);

        user.RoleId = _userRepository.GetRoleIdByRoleName(request.Role);

        var passwordHasher = new PasswordHasher<User>();
        user.Password = passwordHasher.HashPassword(user, request.Password);

        await _userRepository.Create(user);
    }
}