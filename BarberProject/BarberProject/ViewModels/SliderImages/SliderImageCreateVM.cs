using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.SliderImages
{
    public class SliderImageCreateVM
    {
        [Required]
        public List<IFormFile> SliderImages { get; set; }
    }
}
