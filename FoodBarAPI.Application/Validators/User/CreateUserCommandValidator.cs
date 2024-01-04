using FluentValidation;
using FoodBarAPI.Application.Commands;
using FoodBarAPI.Domain.Interfaces;

namespace FoodBarAPI.Application.Validators;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator(IUserRepository _users)
    {
        RuleFor(p => p.Login)
            .NotEmpty()
            .Custom((value, context) =>
            {
                if (_users.Exists(value))
                    context.AddFailure("User exists!");
            });

        RuleFor(u => u.Password)
            .NotEmpty();

        RuleFor(p => p.Role)
            .NotEmpty()
            .Custom((value, context) =>
            {
                if (_users.GetRoleIdByRoleName(value) == 0)
                    context.AddFailure("Role not exists!");
            });
    }
}