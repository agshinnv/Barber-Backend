using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IPricingCategoryService
    {
        Task<IEnumerable<PricingCategory>> GetAll();
        Task<PricingCategory> GetById(int id);
        Task Delete(PricingCategory pricingCategory);
        Task Create(PricingCategory pricingCategory);
        Task Edit(int id, PricingCategory pricingCategory);
    }
}
