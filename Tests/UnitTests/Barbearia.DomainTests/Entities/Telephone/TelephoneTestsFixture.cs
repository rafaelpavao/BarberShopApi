using Barbearia.Domain.Entities;
using Bogus;

namespace UnitTests.Barbearia.DomainTests;

public class TelephoneTestsFixture
{
    // Exemplo de telefone válido
    public Telephone GenerateValidTelephone()
    {
        return new Telephone()
        {
            Number = "47988887777",
            Type = Telephone.TelephoneType.Mobile,
            PersonId = 1
        };
    }

    // Exemplo de telefone invalido
    public Telephone GenerateInvalidTelephone()
    {
        return new Telephone()
        {
            Number = "479888877776",
            Type = Telephone.TelephoneType.Mobile,
            PersonId = 1
        };
    }

    // Exemplo válido com Bogus
    public List<Telephone> BogusGenerateTelephonesWithValidNumber(int quantity)
    {
        var telephoneFaker = new Faker<Telephone>()
            .CustomInstantiator(f => new Telephone
            {
                PersonId = f.UniqueIndex + 1,
                Number = f.Random.Replace("###########"),
                Type = Telephone.TelephoneType.Mobile
            });

        return telephoneFaker.Generate(quantity);
    }

    // Exemplo invalido com Bogus
    public List<Telephone> BogusGenerateTelephonesWithInvalidNumber(int quantity)
    {
        var telephone = new Faker<Telephone>().CustomInstantiator(f => new Telephone()
        {
            PersonId = f.UniqueIndex + 1,
            Number = f.Random.String(),
            Type = Telephone.TelephoneType.Landline
        });

        return telephone.Generate(quantity);
    }

    // Exemplo com Theory, validando massa de dados
    public Telephone GerenateTelephonesWithTheory(string phoneNumber)
    {
        return new Telephone()
        {
            Number = phoneNumber,
            Type = Telephone.TelephoneType.Mobile,
            PersonId = 1
        };
    }

}