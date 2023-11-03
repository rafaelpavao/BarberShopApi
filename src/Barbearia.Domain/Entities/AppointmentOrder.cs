namespace Barbearia.Domain.Entities;

public class AppointmentOrder
{
    public int OrderId { get; set; }
    public int AppointmentId { get; set; }


    private void CheckOrderId()
    {
        if (OrderId <= 0)
        {
            throw new ArgumentException("OrderId deve ser maior que zero.");
        }
    }


    private void CheckAppointmentId()
    {
        if (AppointmentId <= 0)
        {
            throw new ArgumentException("AppointmentId deve ser maior que zero.");
        }
    }

    public void ValidateAppointmentOrder()
    {
        CheckOrderId();
        CheckAppointmentId();
    }
}