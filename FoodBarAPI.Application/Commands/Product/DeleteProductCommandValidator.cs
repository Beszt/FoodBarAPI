using FluentValidation;
using FoodBarAPI.Domain.Interfaces;

namespace FoodBarAPI.Application.Commands;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator(IProductRepository repository)
    {
        RuleFor(p => p.Barcode)
            .Custom((value, context) =>
            {
                if (!repository.Exists(value))
                    context.AddFailure("Product not exists!");
            });
    }
}