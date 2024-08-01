using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.PricingCategories
{
    public class PricingCategoryEditVM
    {
        [Required]
        public string Name { get; set; }
        public string ExistImage { get; set; }
        public IFormFile NewImage { get; set; }
    }
}
