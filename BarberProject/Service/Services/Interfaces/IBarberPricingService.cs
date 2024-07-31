using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IBarberPricingService
    {
        Task<IEnumerable<BarberPricing>> GetAll();
        Task<BarberPricing> GetById(int id);
        Task Delete(BarberPricing barberPricing);
        Task Create(BarberPricing barberPricing);
        Task Edit(int id, BarberPricing barberPricing);
    }
}
