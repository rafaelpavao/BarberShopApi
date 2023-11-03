using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.Features.Appointments.Commands.CreateAppointment
{
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, CreateAppointmentCommandResponse>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateAppointmentCommandHandler> _logger;

        public CreateAppointmentCommandHandler(IItemRepository itemRepository, IPersonRepository personRepository, IMapper mapper, ILogger<CreateAppointmentCommandHandler> logger)
        {
            _itemRepository = itemRepository;
            _personRepository = personRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CreateAppointmentCommandResponse> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            CreateAppointmentCommandResponse response = new();

            var customerFromDatabase = await _personRepository.GetCustomerByIdAsync(request.PersonId);
            if (customerFromDatabase == null)
            {
                response.ErrorType = Error.NotFoundProblem;
                response.Errors.Add("PersonId", new[] { "Customer not found in the database." });
                return response;
            }

            var scheduleFromDatabase = await _personRepository.GetScheduleByIdAsync(request.ScheduleId);
            if (scheduleFromDatabase == null)
            {
                response.ErrorType = Error.NotFoundProblem;
                response.Errors.Add("acheduleId", new[] { "schedule not found in the database." });
                return response;
            }

            var validator = new CreateAppointmentCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if(!validationResult.IsValid)
            {
                response.ErrorType = Error.ValidationProblem;
                response.FillErrors(validationResult);
                return response;
            }

            List<Service> Services = new List<Service>();

            foreach (int services in request.ServicesId)
            {
                var service = await _itemRepository.GetServiceByIdAsync(services);
                if (service == null)
                {
                    response.ErrorType = Error.NotFoundProblem;
                    response.Errors.Add("service", new[] { "service not found in the database." });
                    return response;
                }
                Services.Add(service!); 

            }

            var appointmentEntity = _mapper.Map<Appointment>(request);
            foreach (var service in Services)
            {
                appointmentEntity.Services.Add(service);
            }
            
            try
            {
                appointmentEntity.ValidateAppointment();
            }
            catch (Exception ex)
            {
                response.ErrorType = Error.InternalServerErrorProblem;
                response.Errors.Add("Appointment_Validation", new[] { "Error in appointment validation" });
                _logger.LogError(ex, "erro de validação em create appointment");
                return response;
            }


            _itemRepository.AddAppointment(appointmentEntity);
            await _itemRepository.SaveChangesAsync();

            response.Appointment = _mapper.Map<CreateAppointmentDto>(appointmentEntity);
            return response;
        }
    }
}
