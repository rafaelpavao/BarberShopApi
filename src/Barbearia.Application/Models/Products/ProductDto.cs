using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.Models.Products
{
    public class ProductDto
    {
        public int ItemId { get; set; }
        public string Name{get;set;} = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public int Status { get; set; }
        public string SKU { get; set; } = string.Empty;//verificar se ja tem um sku no sistema ja pra não repetir
        public int QuantityInStock { get; set; }
        public int QuantityReserved { get; set; }
    }
}
