using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ISubscriberService
    {
        Task<IEnumerable<Subscriber>> GetAll();
        Task<Subscriber> GetById(int id);
        Task Delete(Subscriber subscriber);
        Task Create(Subscriber subscriber);
    }
}
