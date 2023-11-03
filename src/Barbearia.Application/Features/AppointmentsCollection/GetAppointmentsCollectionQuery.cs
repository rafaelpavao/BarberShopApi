using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.Features.AppointmentsCollection
{
    public class GetAppointmentsCollectionQuery : IRequest<GetAppointmentsCollectionQueryResponse>
    {
        public string? SearchQuery { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
