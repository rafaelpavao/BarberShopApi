using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.EmployeesCollection;

public class GetEmployeesCollectionQueryHandler : IRequestHandler<GetEmployeesCollectionQuery,GetEmployeesCollectionQueryResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetEmployeesCollectionQueryHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<GetEmployeesCollectionQueryResponse> Handle(GetEmployeesCollectionQuery request, CancellationToken cancellationToken)
    {

        GetEmployeesCollectionQueryResponse response = new();

        var (employeeToReturn, paginationMetadata) = await _personRepository.GetAllEmployeesAsync(request.SearchQuery, request.PageNumber,request.PageSize);        

        response.Employees = _mapper.Map<IEnumerable<GetEmployeesCollectionDto>>(employeeToReturn);
        response.PaginationMetadata = paginationMetadata;

        return response;
    }
}