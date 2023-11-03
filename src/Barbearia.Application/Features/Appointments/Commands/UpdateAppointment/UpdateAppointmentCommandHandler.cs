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

namespace Barbearia.Application.Features.Appointments.Commands.UpdateAppointment
{
    public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, UpdateAppointmentCommandResponse>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateAppointmentCommandHandler> _logger;

        public UpdateAppointmentCommandHandler(IItemRepository itemRepository, IPersonRepository parsonRepository, IMapper mapper, ILogger<UpdateAppointmentCommandHandler> logger)
        {
            _itemRepository = itemRepository;
            _personRepository = parsonRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UpdateAppointmentCommandResponse> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            UpdateAppointmentCommandResponse response = new UpdateAppointmentCommandResponse();

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
                response.Errors.Add("scheduleId", new[] { "schedule not found in the database." });
                return response;
            }

            var appointmentToUpdate = await _itemRepository.GetAppointmentByIdAsync(request.AppointmentId);
            if (appointmentToUpdate == null)
            {
                response.ErrorType = Error.NotFoundProblem;
                response.Errors.Add("appointmentId", new[] { "appointment not found in the database." });
                return response;
            }

            var validator = new UpdateAppointmentCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
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

            _mapper.Map(request, appointmentToUpdate);

            foreach (var service in Services)
            {
                appointmentToUpdate.Services.Add(service);
            }

            try
            {
                appointmentToUpdate.ValidateAppointment();
            }
            catch (Exception ex)
            {
                response.ErrorType = Error.InternalServerErrorProblem;
                response.Errors.Add("Appointment_Validation", new[] { "Error in appointment validation" });
                _logger.LogError(ex, "erro de validação em create appointment");
                return response;
            }

            await _itemRepository.SaveChangesAsync();

            return response;
        }
    }
}
