namespace Barbearia.Domain.Entities;

public class Appointment
{
    public int AppointmentId { get; set; }
    public int ScheduleId { get; set; }
    public Schedule? Schedule { get; set; }
    public int PersonId { get; set; }
    public Person? Person { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime FinishDate { get; set; }
    public int Status { get; set; }
    public DateTime StartServiceDate { get; set; }
    public DateTime FinishServiceDate { get; set; }
    public DateTime ConfirmedDate { get; set; }
    public DateTime CancellationDate { get; set; }
    public List<Service> Services { get; set; } = new();
    public List<Order> Orders { get; set; } = new();



    private void CheckOrder()
    {
        if (Orders.Count > 1) throw new Exception("Appointment só pode estar em uma order");
    }

    private void CheckDateSequence()
    {
        if (StartDate > FinishDate || StartServiceDate > FinishServiceDate)
        {
            throw new Exception("Datas do agendamento estão em uma sequência inválida.");
        }
    }

    private void CheckStatus()
    {
        if (Status < 1 || Status > 3)
        {
            throw new Exception("Status de agendamento inválido.");
        }
    }

    private void CheckCustomerId()
    {
        if (PersonId <= 0)
        {
            throw new Exception("CustomerId inválido.");
        }
    }

    private void CheckScheduleId()
    {
        if (ScheduleId <= 0)
        {
            throw new Exception("ScheduleId inválido.");
        }
    }

    public void ValidateAppointment()
    {
        CheckOrder();
        CheckDateSequence();
        CheckStatus();
        CheckCustomerId();
        CheckScheduleId();
    }

}