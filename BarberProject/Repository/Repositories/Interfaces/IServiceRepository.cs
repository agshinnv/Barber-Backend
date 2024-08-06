using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IServiceRepository : IBaseRepository<Service>
    {
        Task<Service> GetByIdWithImages(int id);
        Task<IEnumerable<Service>> GetAllWithImages();
        Task DeleteImage(ServiceImage image);
        Task<bool> ServiceIsExist(string name);
    }
}
