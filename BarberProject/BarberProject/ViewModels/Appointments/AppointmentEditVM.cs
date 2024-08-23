using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.Appointments
{
    public class AppointmentEditVM
    {
        [Required]
        public string Title { get; set; }
        public string ExistIconImage { get; set; }
        public IFormFile NewIconImage { get; set; }
    }
}
