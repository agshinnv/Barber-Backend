using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.PricingCategories
{
    public class PricingCategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
