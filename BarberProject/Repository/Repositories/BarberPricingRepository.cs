using Domain.Models;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class BarberPricingRepository : BaseRepository<BarberPricing>, IBarberPricingRepository
    {
        public BarberPricingRepository(AppDbContext context) : base(context) { }
    }
}
