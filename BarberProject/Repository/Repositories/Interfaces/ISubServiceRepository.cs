using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface ISubServiceRepository : IBaseRepository<SubService>
    {
        Task<SubService> GetByIdWithServices(int id);
    }
}
