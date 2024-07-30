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
    public class SubscriberService : ISubscriberService
    {
        private readonly ISubscriberRepository _subscriberRepository;

        public SubscriberService(ISubscriberRepository subscriberRepository)
        {
            _subscriberRepository = subscriberRepository;
        }
        public async Task Create(Subscriber subscriber)
        {
            await _subscriberRepository.Create(subscriber);
        }

        public async Task Delete(Subscriber subscriber)
        {
            await _subscriberRepository.Delete(subscriber);
        }

        public async Task<IEnumerable<Subscriber>> GetAll()
        {
            return await _subscriberRepository.GetAll();
        }

        public async Task<Subscriber> GetById(int id)
        {
            return await _subscriberRepository.GetById(id);
        }
    }
}
