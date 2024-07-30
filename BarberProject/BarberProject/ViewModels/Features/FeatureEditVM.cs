using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.Features
{
    public class FeatureEditVM
    {
        [Required]
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public string ExistImage { get; set; }
        public IFormFile NewImage { get; set; }
    }
}
