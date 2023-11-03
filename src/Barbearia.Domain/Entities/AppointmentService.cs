namespace Barbearia.Domain.Entities;

public class AppointmentService
{
    public int AppointmentServiceId { get; set; }
    public int ServiceId { get; set; }
    public int EmployeeId { get; set; }
    public int AppointmentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
    public Decimal CurrentPrice { get; set; }

    //Não tivemos tempo para descobrir como adicionar essas coisas a tabela associativa

    private void CheckServiceId()
    {
        if (ServiceId <= 0)
        {
            throw new ArgumentException("ServiceId deve ser maior que zero.");
        }
    }

    private void CheckEmployeeId()
    {
        if (EmployeeId <= 0)
        {
            throw new ArgumentException("EmployeeId deve ser maior que zero.");
        }
    }

    private void CheckAppointmentId()
    {
        if (AppointmentId <= 0)
        {
            throw new ArgumentException("AppointmentId deve ser maior que zero.");
        }
    }

    private void CheckDurationMinutes()
    {
        if (DurationMinutes <= 0)
        {
            throw new ArgumentException("A duração do serviço deve ser maior que zero.");
        }
    }

    private void CheckCurrentPrice()
    {
        if (CurrentPrice < 0)
        {
            throw new ArgumentException("O preço atual do serviço não pode ser negativo.");
        }
    }

    public void ValidateAppointmentService()
    {
        CheckServiceId();
        CheckEmployeeId();
        CheckAppointmentId();
        CheckDurationMinutes();
        CheckCurrentPrice();
    }
}

