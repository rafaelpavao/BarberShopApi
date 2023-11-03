namespace Barbearia.Domain.Entities;

public class ServiceCategory
{
    public int ServiceCategoryId{get;set;}
    public string Name{get; set;} = string.Empty;
    public List<Service>Services{get;set;} = new();

    private void CheckName()
    {
        if(Name==null || Name ==string.Empty)
        {
            throw new Exception("ServiceCategory must have a name");
        }
        if(Name.Length>80)
        {
            throw new Exception("ServiceCategory's name can't have more than 80 characters");
        }
    }

    public void ValidateServiceCategory()
    {
        CheckName();
    }
}