using System.Data;
using FluentValidation;

namespace Barbearia.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
                .WithMessage("You should fill out a name")
            .MaximumLength(50)
                .WithMessage("The name can only have 50 characters at most");

        RuleFor(p => p.Price)
            .NotEmpty()
                .WithMessage("You should fill out a price")
            .GreaterThan(0)
                .WithMessage("The price must be greater than 0");

        RuleFor(p => p.Description)
            .NotEmpty()
                .WithMessage("You should fill out a description")
            .MaximumLength(200)
                .WithMessage("The description can only have 200 characters at most");

        RuleFor(p => p.Brand)
            .NotEmpty()
                .WithMessage("You should fill out a brand")
            .MaximumLength(50)
                .WithMessage("The brand can only have 50 characters at most");

        RuleFor(p => p.Status)
            .NotEmpty()
                .WithMessage("Status cannot be empty")
            .Must(CheckStatus)
                .WithMessage("Status must be higher than 0 and less than 3");

        RuleFor(p => p.SKU)
            .NotEmpty()
                .WithMessage("SKU cannot be empty");

        RuleFor(p => p.QuantityInStock)
            .NotEmpty()
                .WithMessage("You should fill out a quantity in stock")
            .Must(CheckQuantityInStock)
                .WithMessage("Quantity in stock should be zero or more");

        RuleFor(p => p.ProductCategoryId)
            .NotEmpty()
                .WithMessage("You should fill out a product category");

        RuleFor(p => p.QuantityReserved)
            .NotEmpty()
                .WithMessage("You should fill out quantity reserved")
            .Must(CheckQuantityReserved)
                .WithMessage("Quantity reserved should be zero or more");
    }

    private bool CheckQuantityInStock(int QuantityInStock)
    {
        if (QuantityInStock < 0)
        {
            return false;
        }
        return true;
    }

    private bool CheckQuantityReserved(int QuantityReserved)
    {
        if (QuantityReserved < 0)
        {
            return false;
        }
        return true;
    }
    private bool CheckStatus(int status)
    {
        if(status > 2 || status < 1)
        {
            return false;
        }
        return true;
    }
}