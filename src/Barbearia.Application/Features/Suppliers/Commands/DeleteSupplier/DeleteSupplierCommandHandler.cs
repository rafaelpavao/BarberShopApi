using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Suppliers.Commands.DeleteSupplier;

public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand, bool>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public DeleteSupplierCommandHandler(IPersonRepository personRepository, IMapper mapper){
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplierFromDatabase = await _personRepository.GetSupplierByIdAsync(request.PersonId);

        if(supplierFromDatabase == null) return false;

        _personRepository.DeleteSupplier(supplierFromDatabase);

        return await _personRepository.SaveChangesAsync();
    }
}