using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Suppliers.Queries.GetSupplierById;

public class GetSupplierByIdQueryHandler : IRequestHandler<GetSupplierByIdQuery, GetSupplierByIdDto>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetSupplierByIdQueryHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }
    public async Task<GetSupplierByIdDto> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
    {
        var supplierFromDatabase = await _personRepository.GetSupplierByIdAsync(request.PersonId);

        return _mapper.Map<GetSupplierByIdDto>(supplierFromDatabase);
    }
}