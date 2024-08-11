using BarberProject.ViewModels.Complaints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace BarberProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class ComplaintSuggestController : Controller
    {
        private readonly IComplaintService _complaintService;

        public ComplaintSuggestController(IComplaintService complaintService)
        {
            _complaintService = complaintService; 
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var datas = await _complaintService.GetAll();

            List<ComplaintVM> model = datas.Select(m => new ComplaintVM { Id = m.Id, UserFullName = m.UserFullName, Complaint = m.UserSuggest, UserEmail = m.UserEmail }).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var data = await _complaintService.GetById((int)id);
            if(data is null) return NotFound();

            await _complaintService.Delete(data);

            return RedirectToAction(nameof(Index));
        }


    }
}
