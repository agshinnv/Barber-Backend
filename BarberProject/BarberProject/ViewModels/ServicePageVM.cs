using Domain.Models;

namespace BarberProject.ViewModels
{
    public class ServicePageVM
    {
        public IEnumerable<Domain.Models.Service> Services { get; set; }
        public IEnumerable<Feature> Features { get; set; }
        public Appointment Appointment { get; set; }
    }
}
