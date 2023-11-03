using AutoMapper;
using Barbearia.Application.Features.Schedules.Commands.CreateSchedule;
using Barbearia.Application.Features.Schedules.Commands.UpdateSchedule;
using Barbearia.Application.Features.Schedules.Queries.GetAllSchedules;
using Barbearia.Application.Features.Schedules.Queries.GetScheduleById;
using Barbearia.Application.Features.SchedulesCollection;
using Barbearia.Application.Models;
using Barbearia.Domain.Entities;

namespace Barbearia.Application.Profiles;

public class ScheduleProfile : Profile
{

    public ScheduleProfile()
    {
        CreateMap<Schedule, GetSchedulesCollectionDto>();
        CreateMap<Schedule, GetAllSchedulesDto>();
        CreateMap<Schedule,GetScheduleByIdDto>();

        CreateMap<CreateScheduleCommand, Schedule>();        
        CreateMap<Schedule, CreateScheduleDto>();  

        CreateMap<UpdateScheduleCommand, Schedule>(); 
        CreateMap<Schedule,UpdateScheduleDto>();

        CreateMap<WorkingDayDto,WorkingDay>().ReverseMap();
        CreateMap<PersonDto,Employee>().ReverseMap();

        CreateMap<Schedule,ScheduleForWorkingDayDto>().ReverseMap();
    }
    
}