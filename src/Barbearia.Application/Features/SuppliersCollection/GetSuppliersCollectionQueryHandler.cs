using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.SuppliersCollection;

public class GetSuppliersCollectionQueryHandler : IRequestHandler<GetSuppliersCollectionQuery, GetSuppliersCollectionQueryResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetSuppliersCollectionQueryHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<GetSuppliersCollectionQueryResponse> Handle(GetSuppliersCollectionQuery request, CancellationToken cancellationToken)
    {

        GetSuppliersCollectionQueryResponse response = new();

        var (suppliersToReturn, paginationMetadata) = await _personRepository.GetAllSuppliersAsync(request.SearchQuery, request.PageNumber, request.PageSize);

        response.Suppliers = _mapper.Map<IEnumerable<GetSuppliersCollectionDto>>(suppliersToReturn);
        response.PaginationMetadata = paginationMetadata;

        return response;
    }
}