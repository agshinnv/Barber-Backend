using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepo;
        public ReservationService(IReservationRepository reservationRepo)
        {
            _reservationRepo = reservationRepo;
        }

        public async Task Create(Reservation rez)
        {
            await _reservationRepo.Create(rez);
        }

        public async Task Edit(int id, Reservation rez)
        {
            var existRez = await _reservationRepo.GetById(id);
            existRez.OrderStatus = rez.OrderStatus;
            await _reservationRepo.Edit(existRez);
        }

        public async Task<List<Reservation>> GetAll()
        {
            var datas = await _reservationRepo.GetAllWithIncludes();

            return datas;
        }

        public async Task<Reservation> GetById(int id)
        {
            return await _reservationRepo.GetById(id);
        }
    }
}
