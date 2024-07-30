using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.Appointments
{
    public class AppointmentCreateVM
    {
        public string Title { get; set; }
        public IFormFile IconImage { get; set; }

    }
}
