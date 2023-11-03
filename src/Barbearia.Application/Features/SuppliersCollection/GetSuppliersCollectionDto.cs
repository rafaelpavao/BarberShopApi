using Barbearia.Application.Models;

namespace Barbearia.Application.Features.SuppliersCollection;

public class GetSuppliersCollectionDto
{
    public int PersonId { get; set; }
    public string Name { get; set; } = string.Empty;    
    public string Cnpj { get; set; } = string.Empty;
    public List<TelephoneDto> Telephones { get; set; } = new List<TelephoneDto>();
    public string Status { get; set; } = string.Empty;
}