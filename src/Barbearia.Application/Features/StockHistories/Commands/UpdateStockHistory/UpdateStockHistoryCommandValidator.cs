using FluentValidation;

namespace Barbearia.Application.Features.StockHistories.Commands.UpdateStockHistory;

public class UpdateStockHistoryCommandValidator : AbstractValidator<UpdateStockHistoryCommand>
{
    public UpdateStockHistoryCommandValidator()
    {
        RuleFor(o=>o.Operation)
            .NotEmpty()
                .WithMessage("You should fill out an operation")
            .GreaterThan(0)
                .WithMessage("A operação tem que ser maior que zero");
        
        RuleFor(o=>o.CurrentPrice)
            .NotEmpty()
                .WithMessage("Current price can not be empty")
            .GreaterThan(0)
                .WithMessage("Current price should not be less than 1")
            .LessThanOrEqualTo(999.99m)
                .WithMessage("Current pricemust be less than 1000.00");

        RuleFor(o=>o.Amount)
            .NotEmpty()
                .WithMessage("You should fill out an amount")
            .GreaterThan(0)
                .WithMessage("Amount must be more than zero");

        RuleFor(o=>o.Timestamp)
            .NotEmpty()
                .WithMessage("Timestamp cannot be empty")
            .Must(CheckTimestamp)
                .WithMessage("TimeStamp cant be on the future");

        RuleFor(o=>o.LastStockQuantity)
            .NotEmpty()
                .WithMessage("Last stock quantity cannot be empty");

        RuleFor(o=>o.ProductId)
            .NotEmpty()
                .WithMessage("Product id cannot be empty");

        RuleFor(o=>o.OrderId)
            .Empty()
                .When(o=>o.PersonId != 0 )
                    	.WithMessage("You must person or order id");

        RuleFor(o=>o.PersonId)
            .Empty()
                .When(o=>o.OrderId != 0 )
                    .WithMessage("You must person or order id");   
    }



        private bool CheckTimestamp(DateTime TimeStamp)
        {
            if (TimeStamp > DateTime.UtcNow)
            {
                return false;
            }
            return true;
        }
}