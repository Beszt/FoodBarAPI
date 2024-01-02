using FluentValidation;
using FoodBarAPI.Domain.Interfaces;

namespace FoodBarAPI.Application.Commands;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator(IProductRepository repository)
    {
        RuleFor(p => p.Barcode)
            .NotEmpty()
            .GreaterThan(9999999)
            .LessThan(100000000000000)
            .Custom((value, context) =>
            {
                if (repository.Exists(value))
                    context.AddFailure("Product exists!");
            });

        RuleFor(p => p.Name)
            .NotEmpty();

        RuleFor(p => p.Weight)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(p => p.Energy)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(p => p.Protein)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(p => p.Fat)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(p => p.Carbohydrates)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(p => p.Sugar)
            .GreaterThan(0);

        RuleFor(p => p.Salt)
            .GreaterThan(0);

        RuleFor(p => p.Fiber)
            .GreaterThan(0);
    }
}