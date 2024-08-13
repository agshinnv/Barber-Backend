using BarberProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace BarberProject.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;
        private readonly IWorkTimeService _workTimeService;
        public FooterViewComponent(ISettingService settingService, 
                                   IWorkTimeService workTimeService)
        {
            _settingService = settingService;
            _workTimeService = workTimeService;

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var setting = await _settingService.GetAll();
            var workTimes = await _workTimeService.GetAll();

            Dictionary<string, string> values = new();

            foreach (KeyValuePair<int, Dictionary<string, string>> item in setting)
            {
                values.Add(item.Value["Key"], item.Value["Value"]);
            }

            FooterVM response = new()
            {
                Settings = values,
                WorkTimes = workTimes
            };


            return await Task.FromResult(View(response));
        }
    }
}
