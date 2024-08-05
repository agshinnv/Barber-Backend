using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.SubServices
{
    public class SubServiceEditVM
    {
        [Required]
        public string SubServiceName { get; set; }
        [Required]
        public int SubServicePrice { get; set; }
        public int ServiceId { get; set; }
    }
}
