using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.BarberServices
{
    public class BarberServiceEditVM
    {
        [Required]
        public string ServiceName { get; set; }
        [Required]
        public string ServiceDescription { get; set; }
        [Required]
        public int Price { get; set; }
        public string ExistServiceImage { get; set; }
        public IFormFile NewServiceImage { get; set; }
        public string ExistIconImage { get; set; }
        public IFormFile NewIconImage { get; set; }
    }
}
