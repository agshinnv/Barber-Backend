using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class BarberService : BaseEntity
    {
        public string ServiceImage { get; set; }
        public string IconImage { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public int Price { get; set; }
    }
}
