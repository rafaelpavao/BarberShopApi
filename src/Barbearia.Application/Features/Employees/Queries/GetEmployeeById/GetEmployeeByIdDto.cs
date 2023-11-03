using System.Text.Json.Serialization;
using Barbearia.Application.Models;
using static Barbearia.Domain.Entities.Person;

namespace Barbearia.Application.Features.Employees.Queries.GetEmployeeById;

public class GetEmployeeByIdDto 
{
    public int PersonId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly BirthDate{get;set;}
    public int Gender { get; set; }
    public string Cpf { get; set; } = string.Empty;    
    public string Email { get; set; } = string.Empty;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TypeStatus Status { get; set; }
    public List<TelephoneDto> Telephones {get; set;} = new List<TelephoneDto>();
    public List<AddressDto> Addresses {get; set;} = new List<AddressDto>();
    public List<RoleDto> Roles {get;set;} = new List<RoleDto>();
    public List<ServiceDto> Services {get;set;} = new List<ServiceDto>();
}