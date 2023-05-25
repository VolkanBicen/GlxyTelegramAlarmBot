using Microsoft.AspNetCore.Mvc;
using TelegramAlarmBot.Model;

namespace TelegramAlarmBotWeb.Controllers
{
    public class HomeController : Controller
    {
        //private readonly TelegramAlarmBot.Helper.Data _dataHelper;
        private readonly List<AlarmModel> _result;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _result = new TelegramAlarmBot.Helper.Data().GetData();
            
        }

        public IActionResult Index()
        {
            return View(_result.OrderBy(x => x.Hour).OrderBy(x => x.Repeat).OrderBy(x=>x.Active).ToList());
        }

        [HttpPost]
        public IActionResult Update(string alarm_name,bool status)
        {
            var data = _result.Find(p => p.Alarm_name == alarm_name);
            new TelegramAlarmBot.Helper.Data().DeleteData(data,status);
            return Content("Success");
        }

        public IActionResult CreateAlarm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAlarm(AlarmModel model)
        {
            model.Repeat = DateTime.Parse(model.Repeat).ToString("dd.MM.yyyy");
            model.Active = true;
            new TelegramAlarmBot.Helper.Data().InsertData(model);
            return View();
        }

    }
}