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
    public class FishController : Controller
    {
        public FishController()
        {
        }


        // GET: Fish
        public async Task<IActionResult> Index()
        {
            using (var http = new HttpClient())
            {
                using (var response = await http.GetAsync(Const.APIEndPoint + "Fish"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content.ToString());
                        if (result != null && result.Status == Const.SUCCESS_READ_CODE)
                        {
                            var data = JsonConvert.DeserializeObject<List<Fish>>(result.Data.ToString());
                            return View(data);
                        }

                    }
                }
            }
            return View(new List<Fish>());
        }

        // GET: Fish/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            using (var http = new HttpClient())
            {
                using (var repsone = await http.GetAsync(Const.APIEndPoint + $"Fish/{id}"))
                {
                    if (repsone.IsSuccessStatusCode)
                    {
                        var content = await repsone.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Status == Const.SUCCESS_READ_CODE)
                        {
                            var data = JsonConvert.DeserializeObject<Fish>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new Fish());
        }

        // GET: Fish/Create
        public async Task<IActionResult> Create()
        {
            ViewData["PondId"] = new SelectList(await GetPonds(), "PondId", "PondId");
            ViewData["CustomerId"] = new SelectList(await GetCustomers(), "CustomerId", "FullName");
            return View();
        }

        // POST: Fish/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("FishID,CustomerId,PondID,Name,Breed,DateOfBirth,HealthStatus,LastCheckupDate,Notes,Color,Size,Photo")] Fish fish)
        {
            bool createStatus = false;
            if (ModelState.IsValid)
            {
                using (var http = new HttpClient())
                {
                    using (var repsone = await http.PostAsJsonAsync(Const.APIEndPoint + $"Fish", fish))
                    {
                        if (repsone.IsSuccessStatusCode)
                        {
                            var content = await repsone.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Status == Const.SUCCESS_CREATE_CODE)
                            {
                                createStatus = true;
                            }
                        }
                    }
                }
            }
            if (createStatus)
                return RedirectToAction(nameof(Index));

            ViewData["PondId"] = new SelectList(await GetPonds(), "PondId", "PondId");
            ViewData["CustomerId"] = new SelectList(await GetCustomers(), "CustomerId", "FullName");
            return View(fish);
        }

        // GET: Fishs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var fish = new Fish();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + $"Fish/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Status == Const.SUCCESS_READ_CODE)
                        {
                            fish = JsonConvert.DeserializeObject<Fish>(result.Data.ToString());
                        }
                    }
                }
            }

            ViewData["CustomerId"] = new SelectList(await GetCustomers(), "CustomerId", "FullName");
            ViewData["PondId"] = new SelectList(await GetPonds(), "PondId", "PondId");

            return View(fish);
        }

        // POST: Fishs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("FishID,CustomerId,PondID,Name,Breed,DateOfBirth,HealthStatus,LastCheckupDate,Notes,Color,Size,Photo")] Fish fish)
            {
                bool updateStatus = false;
                if (id != fish.FishId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    using (var http = new HttpClient())
                    {
                        using (var repsone = await http.PostAsJsonAsync(Const.APIEndPoint + $"Fish", fish))
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

                ViewData["CustomerId"] = new SelectList(await GetCustomers(), "CustomerId", "FullName");
                ViewData["PondId"] = new SelectList(await GetPonds(), "PondId", "PondId");
                return View(fish);
            }

        // GET: Fish/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            using (var http = new HttpClient())
            {
                using (var repsone = await http.GetAsync(Const.APIEndPoint + $"Fish/{id}"))
                {
                    if (repsone.IsSuccessStatusCode)
                    {
                        var content = await repsone.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Status == Const.SUCCESS_READ_CODE)
                        {
                            var data = JsonConvert.DeserializeObject<Fish>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new Fish());
        }

        // POST: Fish/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool deleteStatus = false;
            using (var http = new HttpClient())
            {
                using (var repsone = await http.DeleteAsync(Const.APIEndPoint + $"Fish/{id}"))
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

        private async Task<IList<Pond>?> GetPonds()
        {
            using (var http = new HttpClient())
            {
                var response = await http.GetAsync(Const.APIEndPoint + "Ponds");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                    if (result != null && result.Status == Const.SUCCESS_READ_CODE)
                    {
                        var data = JsonConvert.DeserializeObject<IList<Pond>>(result.Data.ToString());
                        return data;
                    }
                }
            }

            return new List<Pond>();
        }

        private async Task<IList<Customer>?> GetCustomers()
        {
            using (var http = new HttpClient())
            {
                var response = await http.GetAsync(Const.APIEndPoint + "Customers");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                    if (result != null && result.Status == Const.SUCCESS_READ_CODE)
                    {
                        var data = JsonConvert.DeserializeObject<IList<Customer>>(result.Data.ToString());
                        return data;
                    }
                }
            }

            return new List<Customer>();
        }

    }
}
