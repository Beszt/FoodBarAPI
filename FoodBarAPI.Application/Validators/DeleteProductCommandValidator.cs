using FluentValidation;
using FoodBarAPI.Application.Commands;
using FoodBarAPI.Domain.Interfaces;

namespace FoodBarAPI.Application.Validators;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator(IProductRepository _products, IUserRepository _users)
    {
        RuleFor(p => p)
            .Custom((value, context) =>
            {
                if (!_products.Exists(value.Barcode))
                    context.AddFailure("Product not exists!");
                else if (!_products.WasCreatedBy(value.Barcode, value.UserId)
                        && !_users.HasAdminRole(value.UserId))
                    context.AddFailure("Yo have not perrmision to do that!");
            });
    }
}