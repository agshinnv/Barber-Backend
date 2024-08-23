using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IReservationService
    {
        Task Create(Reservation rez);
        Task<List<Reservation>> GetAll();
        Task<Reservation> GetById(int id);
        Task Edit(int id, Reservation rez);
    }
}
