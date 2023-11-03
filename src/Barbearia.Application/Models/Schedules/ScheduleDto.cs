using Barbearia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.Models
{
    public class ScheduleDto
    {
        public int ScheduleId { get; set; }
        public int WorkingDayId { get; set; }
        public int Status { get; set; }
    }
}
