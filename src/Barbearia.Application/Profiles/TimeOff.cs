using AutoMapper;
using Barbearia.Application.Features.TimesOff.Commands.CreateTimeOff;
using Barbearia.Application.Features.TimesOff.Commands.UpdateTimeOff;
using Barbearia.Application.Features.TimesOff.Queries.GetTimeOffById;
using Barbearia.Application.Features.WorkingDays.Query.GetWorkingDay;
using Barbearia.Application.Models;
using Barbearia.Domain.Entities;

namespace Barbearia.Application.Profiles;

public class TimeOffProfile : Profile
{
    public TimeOffProfile(){

        CreateMap<TimeOff, GetTimeOffByIdDto>().ReverseMap();
        CreateMap<TimeOff,TimeOffForWorkingDayDto>().ReverseMap();

        CreateMap<CreateTimeOffCommand,TimeOff>().ReverseMap();
        CreateMap<TimeOff,CreateTimeOffDto>().ReverseMap();

        CreateMap<UpdateTimeOffCommand, TimeOff>().ReverseMap();     
        CreateMap<TimeOff,UpdateTimeOffDto>().ReverseMap(); 
    }

}