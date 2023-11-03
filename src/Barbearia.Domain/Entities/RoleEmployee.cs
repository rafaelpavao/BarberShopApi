namespace Barbearia.Domain.Entities;

public class RoleEmployee
{
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public int RoleId { get; set; }
    public Role? Role { get; set; }

    private void CheckEmployeeId()
    {
        if (EmployeeId <= 0)
        {
            throw new ArgumentException("PersonId deve ser maior que zero.");
        }
    }

    private void CheckRoleId()
    {
        if (RoleId <= 0)
        {
            throw new ArgumentException("RoleId deve ser maior que zero.");
        }
    }

    public void ValidateRoleEmployee()
    {
        CheckEmployeeId();
        CheckRoleId();
    }
}