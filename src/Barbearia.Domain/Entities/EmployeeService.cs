namespace Barbearia.Domain.Entities;

public class EmployeeService
{
    public int PersonId { get; set; }
    public int ServiceId { get; set; }

    private void CheckPersonId()
    {
        if (PersonId <= 0)
        {
            throw new ArgumentException("PersonId deve ser maior que zero.");
        }
    }


    private void CheckServiceId()
    {
        if (ServiceId <= 0)
        {
            throw new ArgumentException("ServiceId deve ser maior que zero.");
        }
    }

    public void ValidateAppointmentOrder()
    {
        CheckPersonId();
        CheckServiceId();
    }
}