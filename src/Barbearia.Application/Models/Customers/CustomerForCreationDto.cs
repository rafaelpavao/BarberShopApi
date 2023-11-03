namespace Barbearia.Application.Models;

public class CustomerForCreationDto
{    
    public string Name{get;set;} = string.Empty;
    public DateOnly BirthDate{get;set;}
    public int Gender { get; set; }
    public string Cpf { get; set; } = string.Empty;    
    public string Email { get; set; } = string.Empty;    
    public List<TelephoneDto> Telephones {get; set;} = new List<TelephoneDto>();
    public List<AddressDto> Addresses {get; set;} = new List<AddressDto>();
}