using AutoMapper;
using Barbearia.Application.Features.Telephones.Commands.CreateTelephone;
using Barbearia.Application.Features.Telephones.Commands.UpdateTelephone;
using Barbearia.Application.Features.Telephones.Query.GetTelephone;
using Barbearia.Domain.Entities;

namespace Barbearia.Application.Profiles;

public class TelephoneProfile : Profile
{
    public TelephoneProfile(){
        CreateMap<Telephone, GetTelephoneDto>().ReverseMap();
        CreateMap<CreateTelephoneCommand, Telephone>();
        CreateMap<Telephone, CreateTelephoneDto>();
        CreateMap<UpdateTelephoneCommand, Telephone>();
    }

}