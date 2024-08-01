using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class BarberPricing : BaseEntity
    {
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int PricingCategoryId { get; set; }
        public PricingCategory PricingCategory { get; set; }
    }
}
