﻿using Barbearia.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.Features.Appointments.Queries.GetAllAppointments
{
    public class GetAllAppointmentsQuery : IRequest<IEnumerable<GetAllAppointmentsDto>>
    {
    }
}
