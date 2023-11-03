namespace Barbearia.Domain.Entities;

public class Customer : Person
{

    private void CheckCpf() //Código de validação do CPF.
    {
        // Remove non-numeric characters
        Cpf = Cpf.Replace(".", "").Replace("-", "");

        // Check if it has 11 digits
        if (Cpf.Length != 11)
        {
            throw new ArgumentException("CPF inválido");
        }

        // Check if all digits are the same
        bool allDigitsEqual = true;
        for (int i = 1; i < Cpf.Length; i++)
        {
            if (Cpf[i] != Cpf[0])
            {
                allDigitsEqual = false;
                break;
            }
        }
        if (allDigitsEqual)
        {
            throw new ArgumentException("CPF inválido");
        }

        // Check first verification digit
        int sum = 0;
        for (int i = 0; i < 9; i++)
        {
            sum += int.Parse(Cpf[i].ToString()) * (10 - i);
        }
        int remainder = sum % 11;
        int verificationDigit1 = remainder < 2 ? 0 : 11 - remainder;
        if (int.Parse(Cpf[9].ToString()) != verificationDigit1)
        {
            throw new ArgumentException("CPF inválido");
        }

        // Check second verification digit
        sum = 0;
        for (int i = 0; i < 10; i++)
        {
            sum += int.Parse(Cpf[i].ToString()) * (11 - i);
        }
        remainder = sum % 11;
        int verificationDigit2 = remainder < 2 ? 0 : 11 - remainder;
        if (int.Parse(Cpf[10].ToString()) != verificationDigit2)
        {
            throw new ArgumentException("CPF inválido");
        }

    }


    private void CheckCustomerTelephone()
    {
        if (Telephones.Count == 0)
        {
            throw new ArgumentException("Um cliente deve conter pelo menos um telefone.");
        }
        if (Telephones.Count > 1)
        {
            throw new ArgumentException("Um cliente só pode ter um telefone principal.");
        }
    }

    private void CheckCustomerAddress()
    {
        if (Addresses.Count > 1)
        {
            throw new ArgumentException("Um cliente só pode ter um endereço cadastrado.");
        }
    }

    private void CheckCnpj()
    {
        if (!string.IsNullOrEmpty(Cnpj))
        {
            throw new ArgumentException("Cliente não pode ter CNPJ cadastrado.");
        }
    }

    private void CheckStatus()
    {
        if (Status != 0)
        {
            throw new ArgumentException("Cliente não pode ter status");
        }
    }

    private void CheckBirthDate()
    {
        if(BirthDate> DateOnly.FromDateTime(DateTime.UtcNow)) throw new ArgumentException("BirthDate de cliente não pode ser no futuro");
    }

    public void ValidateCustomer()
    {
        CheckCustomerTelephone();
        CheckCustomerAddress();
        CheckCnpj();
        CheckStatus();
        CheckCpf();
        CheckBirthDate();
    }
}