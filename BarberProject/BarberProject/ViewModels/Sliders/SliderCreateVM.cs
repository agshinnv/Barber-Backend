using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.Sliders
{
    public class SliderCreateVM
    {
        [Required]
        public string SliderTitle { get; set; }
        [Required]
        public string SliderDesc { get; set; }
    }
}
