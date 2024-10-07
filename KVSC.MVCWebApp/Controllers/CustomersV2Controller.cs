using Microsoft.AspNetCore.Mvc;

namespace KVSC.MVCWebApp.Controllers
{
    public class CustomersV2Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
