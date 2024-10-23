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
    public class PondsV2Controller : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
