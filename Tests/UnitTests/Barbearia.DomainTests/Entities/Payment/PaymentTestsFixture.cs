using Barbearia.Domain.Entities;

namespace UnitTests.Barbearia.DomainTests;

public class PaymentTestsFixture
{
    public Payment GenerateValidPayment()
    {
        return new Payment()
        {
            PaymentId = 1,
            BuyDate = DateTime.UtcNow,
            GrossTotal = 80,
            PaymentMethod = "Dinheiro",
            Description = "Hora de testar",
            Status = 1,
            NetTotal = 60,
            OrderId = 1
        };
    }

    public Payment GenerateInvalidPayment()
    {
        return new Payment()
        {
            PaymentId = 1,
            BuyDate = DateTime.Now,
            GrossTotal = 80000000000m,
            PaymentMethod = "Abraços",
            Description = "Como invalidar isso daqui?",
            Status = 400,
            NetTotal = 70000000000000000000m,
            OrderId = 7
        };
    }

    public Payment GerenatePaymentsWithTheory(string paymentMethod)
    {
        return new Payment()
        {
            PaymentId = 1,
            BuyDate = DateTime.UtcNow,
            GrossTotal = 80,
            PaymentMethod = paymentMethod,
            Description = "Hora de testar",
            Status = 1,
            NetTotal = 60,
            OrderId = 1
        };
    }

    //public Payment GenerateValidPaymentBogus()
    //{
    //    var anyValidDate = DateOnly.FromDateTime(new Faker().
    //        Date.Past(80, DateTime.UtcNow.AddYears(-18)));

    //    var payment = new Faker<Payment>("pt_BR")
    //        .CustomInstantiator(f => new Payment()
    //        {
    //            PaymentId = f.UniqueIndex + 1,
    //            BuyDate = DateTime.UtcNow,
    //            GrossTotal = 80,

    //        });.RuleFor(c => c.Email, (f, c) =>
    //        f.Internet.Email(c.Name.ToLower()));
    //    return payment;
    //}

    //public Payment GenerateInvalidPaymentBogus()
    //{
    //    var gender = new Faker().PickRandom<Name.Gender>();
    //    var anyValidDate = DateOnly.FromDateTime(new Faker().
    //        Date.Past(1, DateTime.UtcNow.AddYears(-1)));

    //    var customer = new Faker<Customer>("pt_BR")
    //        .CustomInstantiator(f => new Customer()
    //        {
    //            Id = f.UniqueIndex + 1,
    //            Name = f.Name.FullName(gender),
    //            BirthdayDate = anyValidDate,
    //            CPF = f.Person.Cpf()
    //        }).RuleFor(c => c.Email, (f, c) =>
    //        f.Internet.Email(c.Name.ToLower()));
    //    return customer;
    //}
}

