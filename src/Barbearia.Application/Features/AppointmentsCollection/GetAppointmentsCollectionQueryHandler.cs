using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.Features.AppointmentsCollection
{
    public class GetAppointmentsCollectionQueryHandler : IRequestHandler<GetAppointmentsCollectionQuery, GetAppointmentsCollectionQueryResponse>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public GetAppointmentsCollectionQueryHandler(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task<GetAppointmentsCollectionQueryResponse> Handle(GetAppointmentsCollectionQuery request, CancellationToken cancellationToken)
        {
            GetAppointmentsCollectionQueryResponse response = new GetAppointmentsCollectionQueryResponse();

            var (appointmentToReturn, paginationMetadata) = await _itemRepository.GetAllAppointmentsAsync(request.SearchQuery, request.PageNumber, request.PageSize);

            response.Appointments = _mapper.Map<IEnumerable<GetAppointmentsCollectionDto>>(appointmentToReturn);
            response.PaginationMetadata = paginationMetadata;

            return response;
        }
    }
}
