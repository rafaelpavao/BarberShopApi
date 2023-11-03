namespace Barbearia.Application.Features.Addresses.Commands.CreateAddress;

public class CreateAddressCommandResponse : BaseResponse
{
    public CreateAddressDto Address {get; set;} = default!;
}