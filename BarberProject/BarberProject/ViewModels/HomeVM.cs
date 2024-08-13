using Domain.Models;
using Service.Services;

namespace BarberProject.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<SliderImage> SliderImages { get; set; }
        public About About { get; set; }
        public History History { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<Domain.Models.Service> Services { get; set; }
        public IEnumerable<Domain.Models.BarberService> BarberServices { get; set; }
        public IEnumerable<Feature> Features { get; set; }
        public IEnumerable<PricingCategory> PricingCategories { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public Appointment Appointment { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<BarberPricing> BarberPricings { get; set; }
        public IEnumerable<Position> Positions { get; set; }
        public Dictionary<string, string> Settings { get; set; }
        public IEnumerable<Colleague> Colleagues { get; set; }
        public IEnumerable<WorkTime> WorkTimes { get; set; }

    }
}
