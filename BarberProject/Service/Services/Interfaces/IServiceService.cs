using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<Domain.Models.Service>> GetAll();
        Task<Domain.Models.Service> GetById(int id);
        Task Create(Domain.Models.Service service);
        Task Edit(int id, Domain.Models.Service service);
        Task Delete(Domain.Models.Service service);
        Task DeleteImage(ServiceImage image);
        Task<bool> ServiceIsExist(string serviceName);
    }
}
