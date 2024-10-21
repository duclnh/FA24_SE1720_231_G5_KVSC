using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KVSC.Data.Models;
using KVSC.Common;
using KVSC.Service.Base;
using Newtonsoft.Json;

namespace KVSC.MVCWebApp.Controllers
{
    public class PondsController : Controller
    {
        public PondsController()
        {
        }

        // GET: Ponds
        public async Task<IActionResult> Index()
        {
            using (var http = new HttpClient())
            {
                using (var response = await http.GetAsync(Const.APIEndPoint + "Ponds"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content.ToString());
                        if (result != null && result.Status == Const.SUCCESS_READ_CODE)
                        {
                            var data = JsonConvert.DeserializeObject<List<Pond>>(result.Data.ToString());
                            return View(data);
                        }

                    }
                }
            }
            return View(new List<Pond>());
        }

        // GET: Ponds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            using (var http = new HttpClient())
            {
                using (var repsone = await http.GetAsync(Const.APIEndPoint + $"Ponds/{id}"))
                {
                    if (repsone.IsSuccessStatusCode)
                    {
                        var content = await repsone.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Status == Const.SUCCESS_READ_CODE)
                        {
                            var data = JsonConvert.DeserializeObject<Pond>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new Pond());
        }

        // GET: Ponds/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Ponds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("PondId,PondSize,WaterQuality,LastMaintenanceDate,Capacity,Notes,Location,CreateDate,LastUpdate,IsActive")] Pond pond)
        {
            bool updateStatus = false;
            if (id != pond.PondId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var http = new HttpClient())
                {
                    using (var repsone = await http.PostAsJsonAsync(Const.APIEndPoint + $"Ponds", pond))
                    {
                        if (repsone.IsSuccessStatusCode)
                        {
                            var content = await repsone.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Status == Const.SUCCESS_UPDATE_CODE)
                            {
                                updateStatus = true;
                            }
                        }
                    }
                }
            }
            if (updateStatus)
                return RedirectToAction(nameof(Index));

            return View(pond);
        }

        // GET: Ponds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var pond = new Pond();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + $"Ponds/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Status == Const.SUCCESS_READ_CODE)
                        {
                            pond = JsonConvert.DeserializeObject<Pond>(result.Data.ToString());
                        }
                    }
                }
            }
            return View(pond);
        }

        // POST: Ponds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PondId,PondSize,WaterQuality,LastMaintenanceDate,Capacity,Notes,Location,CreateDate,LastUpdate,IsActive")] Pond pond)

        {
            bool updateStatus = false;
            if (id != pond.PondId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var http = new HttpClient())
                {
                    using (var repsone = await http.PostAsJsonAsync(Const.APIEndPoint + $"Ponds", pond))
                    {
                        if (repsone.IsSuccessStatusCode)
                        {
                            var content = await repsone.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Status == Const.SUCCESS_UPDATE_CODE)
                            {
                                updateStatus = true;
                            }
                        }
                    }
                }
            }
            if (updateStatus)
                return RedirectToAction(nameof(Index));

            return View(pond);
        }

        // GET: Ponds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            using (var http = new HttpClient())
            {
                using (var repsone = await http.GetAsync(Const.APIEndPoint + $"Ponds/{id}"))
                {
                    if (repsone.IsSuccessStatusCode)
                    {
                        var content = await repsone.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Status == Const.SUCCESS_READ_CODE)
                        {
                            var data = JsonConvert.DeserializeObject<Pond>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new Pond());
        }

        // POST: Ponds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool deleteStatus = false;
            using (var http = new HttpClient())
            {
                using (var repsone = await http.DeleteAsync(Const.APIEndPoint + $"Ponds/{id}"))
                {
                    if (repsone.IsSuccessStatusCode)
                    {
                        var content = await repsone.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Status == Const.SUCCESS_DELETE_CODE)
                        {
                            deleteStatus = true;
                        }
                    }
                }
            }

            if (!deleteStatus)
                return View();


            return RedirectToAction(nameof(Index));
        }
    }
}
