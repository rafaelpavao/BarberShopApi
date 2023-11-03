using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public DeleteCustomerCommandHandler(IPersonRepository personRepository, IMapper mapper){
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerFromDatabase = await _personRepository.GetCustomerByIdAsync(request.PersonId);

        if(customerFromDatabase == null) return false;

        _personRepository.DeleteCustomer(customerFromDatabase);

        return await _personRepository.SaveChangesAsync();
    }
}