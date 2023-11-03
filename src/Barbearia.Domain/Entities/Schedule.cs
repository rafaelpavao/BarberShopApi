namespace Barbearia.Domain.Entities;


public class Schedule
{
    public int ScheduleId{get;set;}
    public int WorkingDayId{get;set;}
    public WorkingDay? WorkingDay{get;set;}
    public int Status{get;set;}//1 active, 2 inactive
    public List<Appointment> Appointments{get;set;} = new();

    private void ValidateWorkingDay()
    {
        if(WorkingDayId<=0)
        {
            throw new Exception("Schedule must be part of working day");
        }
    }

    private void ValidateStatus()
    {
        if(Status!=1 && Status!=2)
        {
            throw new Exception("Status must be only active or inactive");
        }
    }

    public void ValidateSchedule()
    {
        ValidateWorkingDay();
        ValidateStatus();
    }
}