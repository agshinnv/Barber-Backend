using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class SubService : BaseEntity
    {
        public string ServiceName { get; set; }
        public int ServicePrice { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
