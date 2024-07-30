using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAll();
        Task<Appointment> GetById(int id);
        Task Delete(Appointment appointment);
        Task Create(Appointment appointment);
        Task Edit(int id, Appointment appointment);
    }
}
