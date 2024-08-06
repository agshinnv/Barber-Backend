using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IBarberService
    {
        Task<IEnumerable<Domain.Models.BarberService>> GetAll();
        Task<Domain.Models.BarberService> GetById(int id);
        Task Delete(Domain.Models.BarberService barberService);
        Task Create(Domain.Models.BarberService barberService);
        Task Edit(int id, Domain.Models.BarberService barberService);
        Task<bool> ServiceIsExist(string serviceName);
    }
}
