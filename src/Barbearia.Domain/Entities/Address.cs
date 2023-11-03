using System.Text.RegularExpressions;

namespace Barbearia.Domain.Entities;

public class Address
{
    public int AddressId { get; set; }
    public string Street { get; set; } = string.Empty;
    public int Number { get; set; }
    public string District { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Cep { get; set; } = string.Empty;
    public string Complement { get; set; } = string.Empty;
    public int PersonId { get; set; }
    public Person? Person { get; set; }

    private void CheckStreet()
    {
        if (string.IsNullOrWhiteSpace(Street))
        {
            throw new ArgumentException("Rua não pode estar vazio.");
        }
    }

    private void CheckNumber()
    {
        if (Number == 0)
        {
            throw new ArgumentException("Numero não pode estar vazio.");
        }
        if (Number <= 0)
        {
            throw new ArgumentException("Número inválido. O Número deve ser maior que zero.");
        }
    }

    private void CheckDistrict()
    {
        if (string.IsNullOrWhiteSpace(District))
        {
            throw new ArgumentException("Bairro não pode estar vazio.");
        }
    }

    private void CheckCity()
    {
        if (string.IsNullOrWhiteSpace(City))
        {
            throw new ArgumentException("Cidade não pode estar vazioo.");
        }
    }

    private void CheckState()
    {
        if (State.Length != 2)
        {
            throw new ArgumentException("O estado deve ter exatamente 2 caracteres (UF).");
        }
    }

    private void CheckCep()
    {
        if (string.IsNullOrEmpty(Cep))
        {
            throw new ArgumentException("Cep não pode estar vazio.");
        }

        if (!Regex.IsMatch(Cep, "^[0-9]{8}$"))
        {
            throw new ArgumentException("CEP inválido. O CEP deve conter exatamente 8 dígitos numéricos.");
        }

        if (Cep.Distinct().Count() == 1)
        {
            throw new ArgumentException("CEP inválido. O CEP não pode ser composto por todos os dígitos iguais.");
        }

        if (Cep.StartsWith("0"))
        {
            throw new ArgumentException("CEP inválido. O CEP não pode começar com o dígito 0.");
        }
    }

    public void ValidateAddress()
    {
        CheckStreet();
        CheckNumber();
        CheckDistrict();
        CheckCity();
        CheckState();
        CheckCep();
    }
}