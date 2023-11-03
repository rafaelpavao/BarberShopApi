using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.Models.StockHistories
{
    public class StockHistoryOrderDto
    {
        public int StockHistoryId { get; set; }
        public int Operation { get; set; }
        public decimal CurrentPrice { get; set; }
        public int Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public int LastStockQuantity { get; set; }
    }
}
