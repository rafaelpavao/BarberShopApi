namespace Barbearia.Domain.Entities;

public class TimeOff
{
    public int TimeOffId{get; set;}
    public int WorkingDayId{get;set;}
    public WorkingDay? WorkingDay{get;set;}
    public TimeOnly StartTime{get;set;}
    public TimeOnly FinishTime{get;set;}

    private void ValidateWorkingDayId()
    {
        if(WorkingDayId<=0)
        {
            throw new Exception("Time Off must be part of a working day");
        }
    }

    private void ValidateDates()
    {
        if(StartTime>FinishTime)
        {
            throw new Exception("Start time must come before Finish time");
        }
    }

    public void ValidateTimeOff()
    {
        ValidateWorkingDayId();
        ValidateDates();
    }
}