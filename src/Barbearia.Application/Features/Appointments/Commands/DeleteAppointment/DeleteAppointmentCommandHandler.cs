using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.Features.Appointments.Commands.DeleteAppointment
{
    public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, bool>
    {
        private readonly IItemRepository _itemRepository;

        public DeleteAppointmentCommandHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<bool> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointmentFromDatabase = await _itemRepository.GetAppointmentByIdAsync(request.AppointmentId);
            
            if (appointmentFromDatabase == null) return false;

            _itemRepository.RemoveAppointment(appointmentFromDatabase);
            return await _itemRepository.SaveChangesAsync();
        }
    }
}
