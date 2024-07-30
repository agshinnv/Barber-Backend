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
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public async Task Create(Appointment appointment)
        {
            await _appointmentRepository.Create(appointment);
        }

        public async Task Delete(Appointment appointment)
        {
            await _appointmentRepository.Delete(appointment);
        }

        public async Task Edit(int id, Appointment appointment)
        {
            var existAppointment = await _appointmentRepository.GetById(id);
            existAppointment.Title = appointment.Title;
            existAppointment.IconImage = appointment.IconImage;

            await _appointmentRepository.Edit(existAppointment);
        }

        public async Task<IEnumerable<Appointment>> GetAll()
        {
            return await _appointmentRepository.GetAll();
        }

        public Task<Appointment> GetById(int id)
        {
            return _appointmentRepository.GetById(id);
        }
    }
}
