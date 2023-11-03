using Barbearia.Application.Contracts.Repositories;
using FluentValidation;

namespace Barbearia.Application.Features.Coupons.Commands.CreateCoupon;

public class CreateCouponCommandValidator : AbstractValidator<CreateCouponCommand>
{
    public CreateCouponCommandValidator()
    {
        RuleFor(c => c.CouponCode)
            .MaximumLength(30)
            .NotNull();

        RuleFor(c => c.CouponCode)
            .NotEmpty()
                .WithMessage("CouponCode should not be empty");

        RuleFor(c => c.DiscountPercent)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(100)
            .WithMessage("Discount Percentage should be between 1 and 100 %.");

        RuleFor(c => c.CreationDate)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Creation Date cannot be in the future.");

        RuleFor(c => c.ExpirationDate)
            .GreaterThan(DateTime.UtcNow)
            .GreaterThan(c => c.CreationDate)
            .WithMessage("Expiration Date must be after creation date.");
    }
}