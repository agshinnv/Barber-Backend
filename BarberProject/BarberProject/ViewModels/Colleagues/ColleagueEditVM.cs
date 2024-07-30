using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.Colleagues
{
    public class ColleagueEditVM
    {
        public string ExistImage { get; set; }
        public IFormFile NewImage { get; set; }
    }
}
