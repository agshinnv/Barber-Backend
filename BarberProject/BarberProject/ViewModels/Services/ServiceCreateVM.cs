using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.Services
{
    public class ServiceCreateVM
    {
        [Required]
        public List<IFormFile> ServiceImages { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile IconImage { get; set; }
    }
}
