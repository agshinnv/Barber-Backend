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
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        public async Task Create(Domain.Models.Service service)
        {
            await _serviceRepository.Create(service);
        }

        public async Task Delete(Domain.Models.Service service)
        {
            await _serviceRepository.Delete(service);
        }

        public async Task DeleteImage(ServiceImage image)
        {
            await _serviceRepository.DeleteImage(image);
        }

        public async Task Edit(int id, Domain.Models.Service service)
        {
            var existService = await _serviceRepository.GetById(id);

            existService.Title = service.Title;
            existService.Description = service.Description;
            existService.IconImage = service.IconImage;
            existService.Content = service.Content;
            existService.ServiceImages = service.ServiceImages;

            await _serviceRepository.Edit(existService);
        }

        public async Task<IEnumerable<Domain.Models.Service>> GetAll()
        {
            return await _serviceRepository.GetAllWithIncludes();
        }

        public async Task<Domain.Models.Service> GetById(int id)
        {
            return await _serviceRepository.GetByIdWithImages(id);
        }

        public async Task<bool> ServiceIsExist(string serviceName)
        {
            return await _serviceRepository.ServiceIsExist(serviceName.Trim());
        }
    }
}
