using Domain.Models;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class PricingCategoryService : IPricingCategoryService
    {
        private readonly IPricingCategoryRepository _pricingCategoryRepository;

        public PricingCategoryService(IPricingCategoryRepository pricingCategoryRepository)
        {
            _pricingCategoryRepository = pricingCategoryRepository;
        }
        public async Task Create(PricingCategory pricingCategory)
        {
            await _pricingCategoryRepository.Create(pricingCategory);
        }

        public async Task Delete(PricingCategory pricingCategory)
        {
            await _pricingCategoryRepository.Delete(pricingCategory);
        }

        public async Task Edit(int id, PricingCategory pricingCategory)
        {
            var existPricingCategory = await _pricingCategoryRepository.GetById(id);

            existPricingCategory.Name = pricingCategory.Name;
            existPricingCategory.Image = pricingCategory.Image;

            await _pricingCategoryRepository.Edit(existPricingCategory);
        }

        public async Task<IEnumerable<PricingCategory>> GetAll()
        {
            return await _pricingCategoryRepository.GetAll();
        }

        public async Task<PricingCategory> GetById(int id)
        {
            return await _pricingCategoryRepository.GetById(id);
        }
    }
}
