using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.PricingCategories
{
    public class PricingCategoryCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
