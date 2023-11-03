using FluentValidation;

namespace Barbearia.Application.Features.Telephones.Commands.CreateTelephone;
public class CreateTelephoneCommandValidator : AbstractValidator<CreateTelephoneCommand>
{
    public CreateTelephoneCommandValidator()
    {
        RuleFor(t => t.PersonId)
               .NotEmpty()
                    .WithMessage("PersonId cannot be empty");

        RuleFor(t => t.Number)
               .NotEmpty()
                    .WithMessage("Telephone number cannot be empty")
                .Must(CheckNumber)
                    .WithMessage("Número de telefone inválido. Use o formato: 47988887777.");

        RuleFor(t => t.Type)                
                .IsInEnum()
                    .WithMessage("Tipo de telefone inválido. O tipo deve ser Móvel ou Fixo.");
    }

    private bool CheckNumber(string number)
    {
        if (!(number.Length == 11 && number.All(char.IsDigit)))
        {
            return false;
        }
        return true;
    }
}