using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KVSC.Data.Models;

namespace KVSC.MVCWebApp.Controllers
{
    public class FishV2Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
