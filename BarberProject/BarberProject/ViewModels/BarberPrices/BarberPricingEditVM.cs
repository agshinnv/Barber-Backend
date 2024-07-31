using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.BarberPrices
{
    public class BarberPricingEditVM
    {
        [Required]
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        [Required]
        public int ServicePrice { get; set; }
    }
}
