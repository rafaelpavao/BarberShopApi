using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.TimesOff.Commands.DeleteTimeOff;

public class DeleteTimeOffCommandHandler : IRequestHandler<DeleteTimeOffCommand, bool>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public DeleteTimeOffCommandHandler(IPersonRepository personRepository, IMapper mapper){
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeleteTimeOffCommand request, CancellationToken cancellationToken)
    {
        var timeOffFromDatabase = await _personRepository.GetTimeOffByIdAsync(request.TimeOffId);

        if(timeOffFromDatabase == null) return false;

        _personRepository.DeleteTimeOff(timeOffFromDatabase);

        return await _personRepository.SaveChangesAsync();
    }
}