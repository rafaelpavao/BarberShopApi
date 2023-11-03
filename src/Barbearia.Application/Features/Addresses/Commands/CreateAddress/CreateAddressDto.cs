namespace Barbearia.Application.Features.Addresses.Commands.CreateAddress;

public class CreateAddressDto
{  
    public int AddressId { get; set; }
    public string Street { get; set; } = string.Empty;
    public int Number { get; set; }
    public string District { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Cep { get; set; } = string.Empty;
    public string Complement { get; set; } = string.Empty;
}