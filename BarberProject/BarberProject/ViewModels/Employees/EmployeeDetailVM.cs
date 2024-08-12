using System.ComponentModel.DataAnnotations;

namespace BarberProject.ViewModels.Employees
{
    public class EmployeeDetailVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmployeeImage { get; set; }
        public string Description { get; set; }
        public string Specialty { get; set; }
        public string SocialIcon { get; set; }
        public string Skill1 { get; set; }
        public string Skill2 { get; set; }
        public string Biography { get; set; }
        public string Education { get; set; }
        public string Awards { get; set; }
        public string ContactDescription { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public string Position { get; set; }
    }
}
