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
    public class SliderRepository : BaseRepository<Slider>, ISliderRepository
    {
        public SliderRepository(AppDbContext context) : base(context) { }


        public async Task <IEnumerable<Slider>> GetAllWithImages()
        {
            return await _entities.IncludeMultiple<Slider>(m=>m.SliderImages).ToListAsync();
        }

        public async Task<Slider> GetByIdWithImages(int id)
        {
            return await _entities.Include(m => m.SliderImages).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task DeleteImage(SliderImage image)
        {
             _context.SliderImages.Remove(image);
            await _context.SaveChangesAsync();
        }

    }
}
