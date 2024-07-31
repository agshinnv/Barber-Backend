using BarberProject.Helpers.Extentions;
using BarberProject.ViewModels.Employees;
using BarberProject.ViewModels.Positions;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Services;
using Service.Services.Interfaces;

namespace BarberProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IWebHostEnvironment _env;
        private readonly IPositionService _positionService;

        public EmployeeController(IEmployeeService employeeService,
                                  IWebHostEnvironment env,
                                  IPositionService positionService)
        {
            _employeeService = employeeService;
            _env = env;
            _positionService = positionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var datas = await _employeeService.GetAll();

            List<EmployeeVM> model = datas.Select(m => new EmployeeVM { Id = m.Id, EmployeeName = m.BarberName }).ToList();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var positions = await _positionService.GetAll();

            ViewBag.positions = new SelectList(positions, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreateVM request)
        {
            var positions = await _positionService.GetAll();

            ViewBag.positions = new SelectList(positions, "Id", "Name");

            if (!ModelState.IsValid) return View();

            if (!request.EmployeeImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("EmployeeImage", "File type must be image");
                return View();
            }

            if (!request.EmployeeImage.CheckFileSize(2))
            {
                ModelState.AddModelError("EmployeeImage", "Image size must be less than 2 Mb");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "-" + request.EmployeeImage.FileName;
            string path = Path.Combine(_env.WebRootPath, "images", fileName);
            await request.EmployeeImage.SaveFileToLocalAsync(path);

            Employee employee = new()
            {
                BarberName = request.Name,
                BarberImage = fileName,
                Description = request.Description,
                Specialty = request.Specialty,
                SocialIcon = request.SocialIcon,
                Skill1 = request.Skill1,
                Skill2 = request.Skill2,
                Biography = request.Biography,
                Education = request.Education,
                Awards = request.Awards,
                ContactDescription = request.ContactDescription,
                Email = request.Email,
                Number = request.Number,
                PositionId = request.PositionId,
            };

            await _employeeService.Create(employee);
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            var employee = await _employeeService.GetById((int)id);
            if (employee is null) return NotFound();

            EmployeeDetailVM model = new()
            {
                EmployeeImage = employee.BarberImage,
                Name = employee.BarberName,
                Description = employee.Description,
                Specialty = employee.Specialty,
                SocialIcon = employee.SocialIcon,
                Skill1 = employee.Skill1,
                Skill2 = employee.Skill2,
                Biography = employee.Biography,
                Education = employee.Education,
                Awards = employee.Awards,
                ContactDescription = employee.ContactDescription,
                Email = employee.Email,
                Number = employee.Number,
                Position = employee.Position.Name
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            Employee existEmployee = await _employeeService.GetById((int)id);
            if (existEmployee is null) return NotFound();

            string existImage = Path.Combine(_env.WebRootPath, "images", existEmployee.BarberImage);
            existImage.DeleteFileFromLocal();

            await _employeeService.Delete(existEmployee);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var positions = await _positionService.GetAll();

            ViewBag.positions = new SelectList(positions, "Id", "Name");

            if (id is null) return BadRequest();

            var existEmployee = await _employeeService.GetById((int)id);

            if (existEmployee is null) return NotFound();

            EmployeeEditVM model = new()
            {
                Name = existEmployee.BarberName,
                ExistImage = existEmployee.BarberImage,
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
                PositionId = existEmployee.PositionId,
            };

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, EmployeeEditVM request)
        {
            var positions = await _positionService.GetAll();

            ViewBag.positions = new SelectList(positions, "Id", "Name");

            if (id is null) return BadRequest();

            var existEmployee = await _employeeService.GetById((int)id);

            if (existEmployee is null) return NotFound();

            if (!ModelState.IsValid) return View();

            if (request.NewImage is not null)
            {
                if (!request.NewImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewImage", "File type must be image");
                    request.ExistImage = existEmployee.BarberImage;
                    return View(request);
                }

                if (!request.NewImage.CheckFileSize(2))
                {
                    ModelState.AddModelError("NewImage", "Image size must be less than 2 Mb");
                    request.ExistImage = existEmployee.BarberImage;
                    return View(request);
                }

                string oldPath = Path.Combine(_env.WebRootPath, "images", existEmployee.BarberImage);
                oldPath.DeleteFileFromLocal();

                string fileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;
                string path = Path.Combine(_env.WebRootPath, "images", fileName);
                await request.NewImage.SaveFileToLocalAsync(path);
                await _employeeService.Edit((int)id, new Employee 

                {
                    BarberName = request.Name,
                    BarberImage = fileName,
                    PositionId = request.PositionId,
                    Description = request.Description,
                    Specialty = request.Specialty,
                    SocialIcon = request.SocialIcon,
                    Skill1 = request.Skill1,
                    Skill2 = request.Skill2,
                    Biography = request.Biography,
                    Education = request.Education,
                    Awards = request.Awards,
                    ContactDescription = request.ContactDescription,
                    Email = request.Email,
                    Number = request.Number,
                });

            }
            else
            {
                await _employeeService.Edit((int)id, new Employee
                {
                    BarberName = request.Name,
                    BarberImage = existEmployee.BarberImage,
                    PositionId = request.PositionId,
                    Description = request.Description,
                    Specialty = request.Specialty,
                    SocialIcon = request.SocialIcon,
                    Skill1 = request.Skill1,
                    Skill2 = request.Skill2,
                    Biography = request.Biography,
                    Education = request.Education,
                    Awards = request.Awards,
                    ContactDescription = request.ContactDescription,
                    Email = request.Email,
                    Number = request.Number,
                });
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
