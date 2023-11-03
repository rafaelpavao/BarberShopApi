using FluentValidation;

namespace Barbearia.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(o => o.Number)
                .NotEmpty()
                    .WithMessage("You should fill out a number")
                .Must(CheckNumber)
                    .WithMessage("Number cannot be negative");

            RuleFor(o => o.Status)
                .NotEmpty()
                    .WithMessage("Status cannot be empty")
                .Must(CheckStatus)
                    .WithMessage("Unsuported Status: Must be between 1 and 3");

            RuleFor(o => o.PersonId)
                .NotEmpty()
                    .WithMessage("You should fill out a person");

            RuleFor(o => o.BuyDate)
                .NotEmpty()
                    .WithMessage("BuyDate cannot be empty")
                .Must(CheckBuyDate)
                    .WithMessage("Date must be valid and not in the future");
        
            RuleFor(o =>o.StockHistoriesOrderId)
            .NotEmpty()
                .WithMessage("stock history order id cannot be empty");

            RuleFor(o => o.ProductsId)
                .NotEmpty()
                    .WithMessage("products id cannot be empty");
            RuleFor(o => o.AppointmentsId)
                .NotEmpty()
                    .WithMessage("appointments id cannot be empty");

        }

        private bool CheckNumber(int Number)
        {
            if (Number == 0)
            {
                return false;
            }
            if (Number <= 0)
            {
                return false;
            }
            return true;
        }


        private bool CheckStatus(int Status)
        {
            if (Status < 1 || Status > 3) return false;

            return true;
        }

        private bool CheckBuyDate(DateTime BuyDate)
        {
            if (!DateTime.TryParse(BuyDate.ToString(), out DateTime parsedDate))
            {
                return false;
            }
            if (parsedDate > DateTime.UtcNow)
            {
                return false;
            }
            return true;
        }
    }
}