namespace Barbearia.Domain.Entities;

public class Role
{
    public int RoleId{get; set;}
    public string Name{get; set;} = string.Empty;
    public List<Employee> Employees {get; set;} = new();

    private void ValidateName()
    {
        if(Name==null || Name ==string.Empty)
        {
            throw new Exception("Role must have a name");
        }
        if(Name.Length>80)
        {
            throw new Exception("Role's name can't have more than 80 characters");
        }
    }

    public void ValidateRole()
    {
        ValidateName();
    }
}