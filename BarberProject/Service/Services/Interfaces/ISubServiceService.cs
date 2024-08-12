using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ISubServiceService
    {
        Task<IEnumerable<SubService>> GetAll();
        Task<SubService> GetById(int id);
        Task<IEnumerable<SubService>> GetSubServicesByService(int id);
        Task Delete(SubService subService);
        Task Create(SubService subService);
        Task Edit(int id, SubService subService);
    }
}
