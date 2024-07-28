using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.Sliders
{
    public class SliderCreateVM
    {
        [Required]
        public List<IFormFile> SliderImages { get; set; }
        [Required]
        public string SliderTitle { get; set; }
        public string SliderDesc { get; set; }
    }
}
