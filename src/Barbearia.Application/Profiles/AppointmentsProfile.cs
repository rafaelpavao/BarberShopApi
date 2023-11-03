using AutoMapper;
using Barbearia.Application.Features.Appointments.Commands.CreateAppointment;
using Barbearia.Application.Features.Appointments.Commands.UpdateAppointment;
using Barbearia.Application.Features.Appointments.Queries.GetAllAppointments;
using Barbearia.Application.Features.Appointments.Queries.GetAppointmentById;
using Barbearia.Application.Features.AppointmentsCollection;
using Barbearia.Application.Models;
using Barbearia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.Profiles
{
    public class AppointmentsProfile : Profile
    {
        public AppointmentsProfile()
        {
            CreateMap<Appointment, GetAllAppointmentsDto>();
            CreateMap<Appointment, GetAppointmentsCollectionDto>();
            CreateMap<Appointment, GetAppointmentByIdDto>();

            CreateMap<CreateAppointmentCommand, Appointment>();
            CreateMap<Appointment, CreateAppointmentDto>();

            CreateMap<UpdateAppointmentCommand, Appointment>();

            CreateMap<Schedule, ScheduleDto>().ReverseMap();
            CreateMap<Service, ServiceDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
        }
        
    }
}
