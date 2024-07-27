using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ServiceImage : BaseEntity
    {
        public string Image { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
