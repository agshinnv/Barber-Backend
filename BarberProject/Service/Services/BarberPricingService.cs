using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class BarberPricingService : IBarberPricingService
    {
        private readonly IBarberPricingRepository _barberPricingRepository;

        public BarberPricingService(IBarberPricingRepository barberPricingRepository)
        {
            _barberPricingRepository = barberPricingRepository;
        }
        public async Task Create(BarberPricing barberPricing)
        {
            await _barberPricingRepository.Create(barberPricing);
        }

        public async Task Delete(BarberPricing barberPricing)
        {
            await _barberPricingRepository.Delete(barberPricing);
        }

        public async Task Edit(int id, BarberPricing barberPricing)
        {
            var existBarberPricing = await _barberPricingRepository.GetById(id);

            existBarberPricing.ServiceName = barberPricing.ServiceName;
            existBarberPricing.Description = barberPricing.Description;
            existBarberPricing.Price = barberPricing.Price;

            await _barberPricingRepository.Edit(existBarberPricing);
        }

        public async Task<IEnumerable<BarberPricing>> GetAll()
        {
            return await _barberPricingRepository.GetAll();
        }

        public async Task<BarberPricing> GetById(int id)
        {
            return await _barberPricingRepository.GetById(id);
        }
    }
}
