using BarberProject.ViewModels.Comments;
using BarberProject.ViewModels.Services;
using BarberProject.ViewModels.SubServices;
using Domain.Models;

namespace BarberProject.ViewModels
{
    public class ServiceDetailPageVM
    {
        public ServiceDetailVM Service { get; set; }
        public List<Domain.Models.Service> Services { get; set; }
        public List<SubService> SubServices { get; set; }
        public SubServiceVM SubServiceData { get; set; }
    }
}
