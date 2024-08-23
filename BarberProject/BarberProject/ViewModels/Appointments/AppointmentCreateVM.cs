using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.Appointments
{
    public class AppointmentCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public IFormFile IconImage { get; set; }

    }
}
