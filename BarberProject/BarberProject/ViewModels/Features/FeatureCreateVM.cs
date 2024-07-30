using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.Features
{
    public class FeatureCreateVM
    {
        [Required]
        public string ServiceName { get; set; }
        public string Description { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
