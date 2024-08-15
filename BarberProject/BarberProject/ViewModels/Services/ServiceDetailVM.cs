using Domain.Models;

namespace BarberProject.ViewModels.Services
{
    public class ServiceDetailVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string IconImage { get; set; }
        public List<ServiceImage> ServiceImages { get; set; }
    }
}
