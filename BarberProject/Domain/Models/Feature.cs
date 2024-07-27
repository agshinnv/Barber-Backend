using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Feature : BaseEntity
    {
        public string Image { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }

    }
}
