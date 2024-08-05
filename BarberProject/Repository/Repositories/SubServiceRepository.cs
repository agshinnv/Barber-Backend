using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class SubServiceRepository : BaseRepository<SubService>, ISubServiceRepository
    {
        public SubServiceRepository(AppDbContext context) : base(context) { }
        
        public async Task<SubService> GetByIdWithServices(int id)
        {
            return await _entities.Include(m => m.Service).FirstOrDefaultAsync(m => m.Id == id);
        }

    }
}
