using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.WorkTimes
{
    public class WorkTimeEditVM
    {
        [Required]
        public string WorkDay { get; set; }
        [Required]
        public string WorkHour { get; set; }

    }
}
