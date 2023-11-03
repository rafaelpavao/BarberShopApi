namespace Barbearia.Domain.Entities;

public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public int Gender { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public TypeStatus Status { get; set; }
    public List<Address> Addresses { get; set; } = new();
    public List<Telephone> Telephones { get; set; } = new();
    public List<Order> Orders { get; set; } = new();
    public List<Product> Products { get; set; } = new();
    public List<StockHistorySupplier> StockHistoriesSupplier { get; set; } = new();
    public List<WorkingDay> WorkingDays { get; set; } = new();
    public List<Role> Roles { get; set; } = new();
    // public List<RoleEmployee> RoleEmployees{get; set;} = new();
    public List<Appointment> Appointments { get; set; } = new();
    public List<Service> Services { get; set; } = new();

    public enum TypeStatus
    {
        NoStatus,
        Active,
        Inactive
    }

    
    private void CheckName()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            throw new ArgumentException("O nome não pode estar vazio ou nulo.");
        }
    }
    
    private void CheckBirthDate()
    {
        if (BirthDate > DateOnly.FromDateTime(DateTime.Now))
        {
            throw new ArgumentException("A data de nascimento não pode estar no futuro.");
        }
    }   
    

    public void ValidatePerson()
    {
        CheckName();
        CheckBirthDate();        
    }

}