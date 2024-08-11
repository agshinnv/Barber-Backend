﻿using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PricingCategory : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public ICollection<BarberPricing> BarberPricings { get; set; }
    }
}
    