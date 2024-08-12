using BarberProject.ViewModels.Employees;
using Domain.Models;

namespace BarberProject.ViewModels
{
    public class TeamDetailPageVM
    {
        public EmployeeDetailVM Employee { get; set; }
        public List<Employee> Employees { get; set; }

    }
}
