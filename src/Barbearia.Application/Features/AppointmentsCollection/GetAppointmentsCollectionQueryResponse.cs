using Barbearia.Application.Features.CustomersCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Barbearia.Application.Features.AppointmentsCollection
{
    public class GetAppointmentsCollectionQueryResponse : BaseResponse
    {
        public IEnumerable<GetAppointmentsCollectionDto> Appointments { get; set; } = default!;
        public PaginationMetadata? PaginationMetadata { get; set; }
        public GetAppointmentsCollectionQueryResponse()
        {
            Errors = new Dictionary<string, string[]>();
        }
    }
}
