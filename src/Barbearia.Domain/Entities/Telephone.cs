namespace Barbearia.Domain.Entities;

public class Telephone
{
    public int TelephoneId { get; set; }
    public string Number { get; set; } = string.Empty;
    public TelephoneType Type { get; set; }
    public int PersonId { get; set; }
    public Person? Person { get; set; }

    private void CheckNumber()
    {
        if (!(Number.Length == 11 && Number.All(char.IsDigit)))
        {
            throw new ArgumentException("Número de telefone inválido. Use o formato: 47988887777.");
        }
    }

    private void CheckType()
    {
        if (Type != TelephoneType.Mobile && Type != TelephoneType.Landline)
        {
            throw new ArgumentException("Tipo de telefone inválido. O tipo deve ser Móvel ou Fixo.");
        }
    }

    public void ValidateTelephone()
    {
        CheckNumber();
        CheckType();
    }

    public enum TelephoneType
    {
        Mobile,
        Landline
    }

}