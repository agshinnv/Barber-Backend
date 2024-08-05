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
    public class SubServiceService : ISubServiceService
    {
        private readonly ISubServiceRepository _subServiceRepository;

        public SubServiceService(ISubServiceRepository subServiceRepository)
        {
            _subServiceRepository = subServiceRepository;
        }
        public async Task Create(SubService subService)
        {
            await _subServiceRepository.Create(subService);
        }

        public async Task Delete(SubService subService)
        {
            await _subServiceRepository.Delete(subService);
        }

        public async Task Edit(int id, SubService subService)
        {
            var existSubService = await _subServiceRepository.GetById(id);

            existSubService.ServiceName = subService.ServiceName;
            existSubService.ServicePrice = subService.ServicePrice;
            existSubService.ServiceId = subService.ServiceId;

            await _subServiceRepository.Edit(existSubService);
        }

        public async Task<IEnumerable<SubService>> GetAll()
        {
            return await _subServiceRepository.GetAll();
        }

        public async Task<SubService> GetById(int id)
        {
            return await _subServiceRepository.GetByIdWithServices(id);
        }
    }
}
