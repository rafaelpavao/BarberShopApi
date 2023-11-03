using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.CustomersCollection;

public class GetCustomersCollectionQueryHandler : IRequestHandler<GetCustomersCollectionQuery,GetCustomersCollectionQueryResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetCustomersCollectionQueryHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<GetCustomersCollectionQueryResponse> Handle(GetCustomersCollectionQuery request, CancellationToken cancellationToken)
    {

        GetCustomersCollectionQueryResponse response = new();

        var (customerToReturn, paginationMetadata) = await _personRepository.GetAllCustomersAsync(request.SearchQuery, request.PageNumber,request.PageSize);
        // if(customerToReturn == null || !customerToReturn.Any())
        // {
        //     response.Errors.Add(
        //         "Customers",
        //         new string[] { "No customers were found in the database." }
        //     );

            
        //     response.ErrorType = Error.NotFoundProblem;
        //     return response;

        // }
        //uma lista nunca deve retornar not found, pois foi o usuario que inseriu os
        //parametros de busca, e o front end deve apenas lhe informar que nada foi encontrado,
        //não é um erro da aplicação.

        response.Customers = _mapper.Map<IEnumerable<GetCustomersCollectionDto>>(customerToReturn);
        response.PaginationMetadata = paginationMetadata;

        return response;
    }
}