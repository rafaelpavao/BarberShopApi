namespace Barbearia.Domain.Entities;

public class WorkingDay
{
    public int WorkingDayId{get; set;}
    public int PersonId{get; set;}
    public Person? Employee{get; set;}
    public DateOnly WorkDate{get; set;}
    public TimeOnly StartTime{get;set;}
    public TimeOnly FinishTime{get;set;}
    public List<TimeOff> TimeOffs = new();
    public Schedule? Schedule{get; set;}



    private void ValidatePerson()
    {
        if(PersonId<=0)
        {
            throw new Exception("Person Id must be valid");
        }
    }

    private void ValidateDates()
    {
        if(StartTime>FinishTime)
        {
            throw new Exception("Start time must come before Finish time");
        }
    }

    public void ValidateWorkingDay()
    {
        ValidatePerson();
        ValidateDates();
    }
}