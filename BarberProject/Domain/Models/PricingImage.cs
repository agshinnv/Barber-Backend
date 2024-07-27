using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PricingImage : BaseEntity
    {
        public string Image { get; set; }
        public string ImageName { get; set; }
    }
}
