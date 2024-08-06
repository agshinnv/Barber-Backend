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
    public class BarberServiceRepository : BaseRepository<BarberService>, IBarberServiceRepository
    {
        public BarberServiceRepository(AppDbContext context) : base(context) { }

        public async Task<bool> ServiceIsExist(string name)
        {
            return await _context.BarberServices.AnyAsync(m => m.ServiceName == name.Trim());
        }
    }
}
