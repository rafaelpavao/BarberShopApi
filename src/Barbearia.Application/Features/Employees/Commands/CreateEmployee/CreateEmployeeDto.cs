using System.Text.Json.Serialization;
using Barbearia.Application.Models;
using Barbearia.Domain.Entities;
using static Barbearia.Domain.Entities.Person;

namespace Barbearia.Application.Features.Employees.Commands.CreateEmployee;

public class CreateEmployeeDto
{
    public int PersonId {get;set;}
    public string Name{get;set;} = string.Empty;
    public DateOnly BirthDate{get;set;}
    public int Gender { get; set; }
    public string Cpf { get; set; } = string.Empty;    
    public string Email { get; set; } = string.Empty;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TypeStatus Status { get; set; }
    public List<TelephoneDto> Telephones { get; set; } = new();
    public List<AddressDto> Addresses { get; set; } = new();
    public List<RoleDto> Roles {get;set;} = new();
    public List<ServiceDto> Services {get;set;} = new();
}