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
    public class AboutRepsitory : BaseRepository<About>, IAboutRepository
    {
        public AboutRepsitory(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<About>> GetAllWithImages()
        {
            return await _entities.IncludeMultiple<About>(m => m.AboutImages).ToListAsync();
        }

        public async Task<About> GetByIdWithImages(int id)
        {
            return await _entities.Include(m => m.AboutImages).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task DeleteImage(AboutImage image)
        {
            _context.AboutImages.Remove(image);
            await _context.SaveChangesAsync();
        }
    }
}
