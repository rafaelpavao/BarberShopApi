namespace Barbearia.Application.Models;

public class ServiceForServiceCategoryQueriesDto
{
    public int DurationMinutes{get;set;}
    public int ServiceCategoryId{get;set;}
    public string Name {get;set;} = string.Empty;
    public int Price{get;set;}

}