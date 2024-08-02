using BarberProject.ViewModels.Abouts;
using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.Services
{
    public class ServiceEditVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public List<ServiceEditImageVM> ExistServiceImages { get; set; }
        public List<IFormFile> NewServiceImages { get; set; }
        public string ExistIconImage { get; set; }
        public IFormFile NewIconImage { get; set; }
    }
}
