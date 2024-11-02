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
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Reservation>> GetAllWithIncludes()
        {
            return await _entities.Include(m=>m.Service).Include(m=>m.User).Include(m=>m.Employee).ToListAsync();
        }

        public async Task<Reservation> GetByIdWithIncludes(int id)
        {
            return await _entities.Include(m => m.Service).Include(m => m.User).Include(m => m.Employee).FirstOrDefaultAsync(m=>m.Id == id);
        }
    }
}
