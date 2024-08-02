using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.BarberServices
{
    public class BarberServiceCreateVM
    {
        [Required]
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public IFormFile ServiceImage { get; set; }
        [Required]
        public IFormFile IconImage { get; set; }
    }
}
