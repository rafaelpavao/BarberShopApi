using System.ComponentModel.Design;

namespace Barbearia.Domain.Entities;

public class Employee: Person
{    
    private void CheckEmployeeTelephone()
    {
        if (Telephones.Count < 1)
        {
            throw new ArgumentException("An employee needs atleast one telephone ");
        }
    }

    private void CheckEmployeeAddress()
    {
        if (Addresses.Count < 1)
        {
            throw new ArgumentException("An employee needs at least one address");
        }
    }

    private void CheckCnpj()
    {
        if (!string.IsNullOrEmpty(Cnpj))
        {
            throw new ArgumentException("An employee cant have a Cnpj");
        }
    }

    private void ValidateCPF() 
    {
        Cpf = Cpf.Replace(".", "").Replace("-", "");

        if (Cpf.Length != 11)
        {
            throw new Exception("Cpf lenght must be right");
        }

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
            throw new Exception("Cpf must be valid");
        }

        int sum = 0;
        for (int i = 0; i < 9; i++)
        {
            sum += int.Parse(Cpf[i].ToString()) * (10 - i);
        }
        int remainder = sum % 11;
        int verificationDigit1 = remainder < 2 ? 0 : 11 - remainder;
        if (int.Parse(Cpf[9].ToString()) != verificationDigit1)
        {
            throw new Exception("Cpf must be valid");
        }

        sum = 0;
        for (int i = 0; i < 10; i++)
        {
            sum += int.Parse(Cpf[i].ToString()) * (11 - i);
        }
        remainder = sum % 11;
        int verificationDigit2 = remainder < 2 ? 0 : 11 - remainder;
        if (int.Parse(Cpf[10].ToString()) != verificationDigit2)
        {
            throw new Exception("Cpf must be valid");
        }

    }

    private void CheckPersonAttributes()
    {
        if(Orders.Count>0)
        {
            throw new Exception("Employee cant have orders");
        }
        if(Products.Count>0)
        {
            throw new Exception("Employee cant have products");
        }
        if(StockHistoriesSupplier.Count>0)
        {
            throw new Exception("Employee cant have stock histories");
        }
    }  
        private void CheckBirthDate()
    {
        if(BirthDate> DateOnly.FromDateTime(DateTime.UtcNow)) throw new ArgumentException("BirthDate de funcionário não pode ser no futuro");
    } 

    public void ValidateEmployee()
    {
        CheckEmployeeTelephone();
        CheckEmployeeAddress();
        CheckCnpj();
        ValidateCPF();
        CheckPersonAttributes();
        CheckBirthDate();
    }
}