using System.Data;
using FluentValidation;

namespace Barbearia.Application.Features.Services.Commands.CreateService;

public class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
{
    public CreateServiceCommandValidator()
    {

        RuleFor(s => s.ServiceCategoryId)
            .NotEmpty()
                .WithMessage("You should fill out a Service category")
            .GreaterThan(0)
                .WithMessage("The id must be positive");

        RuleFor(s => s.DurationMinutes)
            .NotEmpty()
                .WithMessage("You should fill out a Duration minutes")
            .GreaterThan(0)
                .WithMessage("Duration must be greater than 0");        

        RuleFor(s => s.Price)
            .NotEmpty()
                .WithMessage("You should fill out a Price")
            .GreaterThan(0)
                .WithMessage("Price should not be less than 1")
            .LessThanOrEqualTo(999.99m)
                .WithMessage("Current pricemust be less than 1000.00");

        RuleFor(s => s.Description)
            .NotEmpty()
                .WithMessage("You should fill out a Description");

        RuleFor(s => s.Name)
            .NotEmpty()
                .WithMessage("You should fill out a Name");
    }

    // private bool CheckPrice(Decimal Price)
    // {
    //     if (Price <= 0)
    //     {
    //         return false;
    //     }
    //     return true;
    // }
}