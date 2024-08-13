using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Helpers.Extensions;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ServiceRepository : BaseRepository<Service>, IServiceRepository
    {
        public ServiceRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Service>> GetAllWithImages()
        {
            return await _entities.IncludeMultiple<Service>(m => m.ServiceImages).ToListAsync();
        }

        public async Task<IEnumerable<Service>> GetAllWithIncludes()
        {
            return await _entities.Include(m=>m.ServiceImages).Include(m=>m.SubServices).ToListAsync();
        }

        public async Task<Service> GetByIdWithImages(int id)
        {
            return await _entities.Include(m => m.ServiceImages).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task DeleteImage(ServiceImage image)
        {
            _context.ServiceImages.Remove(image);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ServiceIsExist(string name)
        {
            return await _context.Services.AnyAsync(m => m.Title.Trim() == name.Trim());
        }

    }
}
