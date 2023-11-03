using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.WorkingDays.Query.GetWorkingDay;

public class GetWorkingDayQueryHandler : IRequestHandler<GetWorkingDayQuery, GetWorkingDayQueryResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetWorkingDayQueryHandler(IPersonRepository personRepository, IMapper mapper){
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<GetWorkingDayQueryResponse> Handle(GetWorkingDayQuery request, CancellationToken cancellationToken)
    {
        GetWorkingDayQueryResponse response = new();

        var employeeFromDatabase = await _personRepository.GetEmployeeByIdAsync(request.PersonId);
        if (employeeFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("PersonId", new[] { "Employee Not found in database" });
            return response;
        }

        var workingDaysFromDatabase = await _personRepository.GetWorkingDayAsync(request.PersonId);
        response.WorkingDay = _mapper.Map<IEnumerable<GetWorkingDayDto>>(workingDaysFromDatabase);
        return response;
    }
}