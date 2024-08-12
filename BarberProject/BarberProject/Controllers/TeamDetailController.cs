using BarberProject.ViewModels;
using BarberProject.ViewModels.Employees;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace BarberProject.Controllers
{
    public class TeamDetailController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public TeamDetailController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            if (id is null) return BadRequest();

            var existEmployee = await _employeeService.GetById((int)id);

            if(existEmployee is null) return NotFound();


            EmployeeDetailVM employee = new()
            {
                Id = existEmployee.Id,
                EmployeeImage = existEmployee.BarberImage,
                Name = existEmployee.BarberName,
                Description = existEmployee.Description,
                Specialty = existEmployee.Specialty,
                SocialIcon = existEmployee.SocialIcon,
                Skill1 = existEmployee.Skill1,
                Skill2 = existEmployee.Skill2,
                Biography = existEmployee.Biography,
                Education = existEmployee.Education,
                Awards = existEmployee.Awards,
                ContactDescription = existEmployee.ContactDescription,
                Email = existEmployee.Email,
                Number = existEmployee.Number,
                Position = existEmployee.Position.Name
            };

            IEnumerable<Employee> employees = await _employeeService.GetAll();

            TeamDetailPageVM model = new()
            {
                Employee = employee,
                Employees = employees.ToList()
            };

            return View(model);
        }
    }
}
