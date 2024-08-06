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
    public class BarberService : IBarberService
    {
        private readonly IBarberServiceRepository _barberServiceRepository;

        public BarberService(IBarberServiceRepository barberServiceRepository)
        {
            _barberServiceRepository = barberServiceRepository;
        }
        public async Task Create(Domain.Models.BarberService barberService)
        {
            await _barberServiceRepository.Create(barberService);
        }

        public async Task Delete(Domain.Models.BarberService barberService)
        {
            await _barberServiceRepository.Delete(barberService);
        }

        public async Task Edit(int id, Domain.Models.BarberService barberService)
        {
            var existBarberService = await _barberServiceRepository.GetById(id);

            existBarberService.ServiceName = barberService.ServiceName;
            existBarberService.ServiceDescription = barberService.ServiceDescription;
            existBarberService.Price = barberService.Price;
            existBarberService.ServiceImage = barberService.ServiceImage;
            existBarberService.IconImage = barberService.IconImage;

            await _barberServiceRepository.Edit(existBarberService);
        }

        public async Task<IEnumerable<Domain.Models.BarberService>> GetAll()
        {
            return await _barberServiceRepository.GetAll();
        }

        public async Task<Domain.Models.BarberService> GetById(int id)
        {
            return await _barberServiceRepository.GetById(id);
        }

        public Task<bool> ServiceIsExist(string serviceName)
        {
            return _barberServiceRepository.ServiceIsExist(serviceName);
        }
    }
}
