using Domain.Models;

namespace BarberProject.ViewModels
{
    public class AboutPageVM
    {
        public About About { get; set; }
        public History History { get; set; }
        public IEnumerable<Domain.Models.Service> Services { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<Position> Positions { get; set; }
    }
}
