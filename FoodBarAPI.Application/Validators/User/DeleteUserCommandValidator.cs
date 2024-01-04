using FluentValidation;
using FoodBarAPI.Application.Commands;
using FoodBarAPI.Domain.Interfaces;

namespace FoodBarAPI.Application.Validators;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator(IUserRepository _users)
    {
        RuleFor(p => p.Login)
            .NotEmpty()
            .Custom((value, context) =>
            {
                if (!_users.Exists(value))
                    context.AddFailure("User not exists!");
            });
    }
}