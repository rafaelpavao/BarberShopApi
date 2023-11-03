using FluentValidation;

namespace Barbearia.Application.Features.Payments.Commands.UpdatePayment;

public class UpdatePaymentCommandValidator : AbstractValidator<UpdatePaymentCommand>
{
    public UpdatePaymentCommandValidator()
    {

        RuleFor(p => p.GrossTotal)
            .NotEmpty()
                .WithMessage("The gross total cannot be empty")
            .Must(CheckGrossTotal)
                .WithMessage("The gross total must be positive");

        RuleFor(p => p.PaymentMethod)
            .NotEmpty()
                .WithMessage("The payment method cannot be empty")
            .Must(IsPaymentValid)
                .WithMessage("The payment must be 'Crédito', 'Débito' ou 'Dinheiro'");

        RuleFor(p => p.Description)
            .MaximumLength(200)
                .WithMessage("The description can only have 200 characters at most");

        RuleFor(p => p.Status)
            .NotEmpty()
                .WithMessage("The status cannot be empty")
            .Must(CheckStatus)
                .WithMessage("Status must be higher than 0 and less than 4");

        RuleFor(p => p.NetTotal)
            .NotEmpty()
                .WithMessage("The net total cannot be empty")
            .Must(CheckNetTotal)
                .WithMessage("The net total must be positive");

        // RuleFor(p => p.CouponId)
        //     .NotEmpty()
        //         .WithMessage("The buy date cannot be empty");

        //coupon é opcional

        RuleFor(p => p.OrderId)
            .NotEmpty()
                .WithMessage("The order id cannot be empty");

    }

    bool IsPaymentValid(string paymentMethod)
    {

        if (paymentMethod != "Débito" && paymentMethod != "Crédito" && paymentMethod != "Dinheiro")
        {
            return false;
        }

        return true;

    }    

    private bool CheckGrossTotal(Decimal GrossTotal)
    {
        if (GrossTotal <= 0)
        {
            return false;
        }
        return true;
    }


    private bool CheckNetTotal(Decimal NetTotal)
    {
        if (NetTotal <= 0)
        {
            return false;
        }
        return true;
    }

    private bool CheckStatus(int status)
    {
        if (status > 3 || status < 1)
        {
            return false;
        }
        return true;
    }

}