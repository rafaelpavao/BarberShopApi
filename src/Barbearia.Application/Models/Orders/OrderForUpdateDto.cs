using Barbearia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.Models.Orders
{
    public class OrderForUpdateDto
    {
        public int OrderId { get; set; }
        public int PersonId { get; set; }
        public int Number { get; set; }
        public int Status { get; set; }
        public DateTime BuyDate { get; set; }
        public List<StockHistoryOrder> StockHistoriesOrder { get; set; } = new();
        public List<Product> Products { get; set; } = new();
        public List<Appointment> Appointments { get; set; } = new();
    }
}
