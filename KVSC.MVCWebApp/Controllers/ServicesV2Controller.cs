using Microsoft.AspNetCore.Mvc;

namespace KVSC.MVCWebApp.Controllers
{
    public class ServicesV2Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
