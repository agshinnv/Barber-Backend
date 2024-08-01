using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.WorkTimes
{
    public class WorkTimeVM
    {
        public int Id { get; set; }
        public string WorkDay { get; set; }
        public string WorkHour { get; set; }
    }
}
