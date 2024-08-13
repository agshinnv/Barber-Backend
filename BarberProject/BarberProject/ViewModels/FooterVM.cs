using Domain.Models;

namespace BarberProject.ViewModels
{
    public class FooterVM
    {
        public Dictionary<string, string> Settings { get; set; }
        public IEnumerable<WorkTime> WorkTimes { get; set; }
    }
}
