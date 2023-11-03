using AutoMapper;
using Barbearia.Application.Features.WorkingDays.Commands.CreateWorkingDay;
using Barbearia.Application.Features.WorkingDays.Commands.UpdateWorkingDay;
using Barbearia.Application.Features.WorkingDays.Query.GetWorkingDay;
using Barbearia.Domain.Entities;

namespace Barbearia.Application.Profiles;

public class WorkingDaysProfile : Profile
{
    public WorkingDaysProfile(){
        CreateMap<WorkingDay, GetWorkingDayDto>().ReverseMap();
        CreateMap<CreateWorkingDayCommand, WorkingDay>().ReverseMap();
        CreateMap<WorkingDay, CreateWorkingDayDto>().ReverseMap();
        CreateMap<UpdateWorkingDayCommand, WorkingDay>();
    }

}