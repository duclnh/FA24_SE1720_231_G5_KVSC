using Microsoft.AspNetCore.Mvc;

namespace KVSC.MVCWebApp.Controllers
{
    public class DoctorV2Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
