namespace Barbearia.Domain.Entities;

public class Service:Item
{
    public int DurationMinutes{get;set;}
    public int ServiceCategoryId{get;set;}
    public ServiceCategory? ServiceCategory{get;set;}
    public List<Appointment> Appointments{get;set;} = new();
    public List<Person> Persons{get;set;} = new();

    private void CheckDurationMinutes()
    {
        if(DurationMinutes <=0 ) throw new Exception("A service needs duration"); 
    }

    private void CheckServiceCategory()
    {
        if(ServiceCategoryId <=0 ) throw new Exception("A service needs a category");
    }

    private void CheckPrice()
    {
        if (Price <= 0)
        {
            throw new Exception("The price must not be negative");
        }
    }

    public void ValidateService()
    {   
        CheckDurationMinutes();
        CheckServiceCategory();
        CheckPrice();
    }
}