namespace Barbearia.Application.Models;

public class ItemDto
{
    public int ItemId {get;set;}
    public string Name {get;set;} = string.Empty;
    public decimal Price{get;set;}
    public string Description {get;set;} = string.Empty;
}