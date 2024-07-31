using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        public readonly IEmployeeRepository _employeeRespistory;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRespistory = employeeRepository;
        }
        public async Task Create(Employee employee)
        {
            await _employeeRespistory.Create(employee);
        }

        public async Task Delete(Employee employee)
        {
            await _employeeRespistory.Delete(employee);
        }

        public async Task Edit(int id, Employee employee)
        {
            var existEmployee = await _employeeRespistory.GetById(id);

            existEmployee.BarberImage = employee.BarberImage;
            existEmployee.PositionId = employee.PositionId;
            existEmployee.BarberName = employee.BarberName;
            existEmployee.Specialty = employee.Specialty;
            existEmployee.Description = employee.Description;
            existEmployee.SocialIcon = employee.SocialIcon;
            existEmployee.Skill1 = employee.Skill1;
            existEmployee.Skill2 = employee.Skill2;
            existEmployee.Biography = employee.Biography;
            existEmployee.Education = employee.Education;
            existEmployee.Awards = employee.Awards;
            existEmployee.ContactDescription = employee.ContactDescription;
            existEmployee.Email = employee.Email;
            existEmployee.Number = employee.Number;

            await _employeeRespistory.Edit(existEmployee);

        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _employeeRespistory.GetAll();
        }

        public Task<Employee> GetById(int id)
        {
            return _employeeRespistory.GetByIdWithPositions(id);
        }
    }
}
