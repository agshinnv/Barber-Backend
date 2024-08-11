using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IAboutRepository : IBaseRepository<About>
    {
        Task<About> GetByIdWithImages(int id);
        Task<About> GetAllWithImages();
        Task DeleteImage(AboutImage image);
    }
}
