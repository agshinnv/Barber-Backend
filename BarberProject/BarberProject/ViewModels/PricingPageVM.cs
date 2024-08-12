using Domain.Models;

namespace BarberProject.ViewModels
{
    public class PricingPageVM
    {
        public IEnumerable<BarberPricing> BarberPricings { get; set; }
        public IEnumerable<PricingCategory> PricingCategories { get; set; }
        public Appointment Appointment { get; set; }
    }
}
