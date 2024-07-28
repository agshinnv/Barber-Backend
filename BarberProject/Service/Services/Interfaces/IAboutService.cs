using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IAboutService
    {
        Task<IEnumerable<About>> GetAllAsync();
        Task<About> GetByIdAsync(int id);
        Task Create(About about);
        Task EditAsync(int id, About about);
        Task DeleteAsync(About about);
        Task DeleteImage(AboutImage image);
    }
}
