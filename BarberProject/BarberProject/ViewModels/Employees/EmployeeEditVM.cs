using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.Employees
{
    public class EmployeeEditVM
    {
        [Required]
        public string Name { get; set; }
        public string ExistImage { get; set; }
        public IFormFile NewImage { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Specialty { get; set; }
        public string SocialIcon { get; set; }
        [Required]
        public string Skill1 { get; set; }
        public string Skill2 { get; set; }
        [Required]
        public string Biography { get; set; }
        public string Education { get; set; }
        public string Awards { get; set; }
        public string ContactDescription { get; set; }
        public string Email { get; set; }
        [Required]
        public string Number { get; set; }
        public int PositionId { get; set; }
    }
}
