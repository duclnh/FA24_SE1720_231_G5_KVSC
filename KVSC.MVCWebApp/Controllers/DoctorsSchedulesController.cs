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
using System.Numerics;

namespace KVSC.MVCWebApp.Controllers
{
    public class DoctorsSchedulesController : Controller
    {
        private readonly FA24_SE1720_231_G5_KVSCContext _context;

        public DoctorsSchedulesController(FA24_SE1720_231_G5_KVSCContext context)
        {
            _context = context;
        }

        // GET: DoctorsSchedules
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "DoctorsSchedules"))
                {

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<DoctorsSchedule>>
                                (result.Data.ToString());

                            return View(data);
                        }
                    }
                }
            }

            return View(new List<DoctorsSchedule>());
        }

        // GET: DoctorsSchedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            using (var http = new HttpClient())
            {
                using (var repsone = await http.GetAsync(Const.APIEndPoint + $"DoctorsSchedules/{id}"))
                {
                    if (repsone.IsSuccessStatusCode)
                    {
                        var content = await repsone.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Status == Const.SUCCESS_READ_CODE)
                        {
                            var data = JsonConvert.DeserializeObject<DoctorsSchedule>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new ServiceRequest());
        }

        // GET: DoctorsSchedules/Create
        public async Task<IActionResult> Create()
        {
            ViewData["DoctorId"] = new SelectList(await GetDoctors(), "DoctorId", "FullName");
          
            return View();
        }

        // POST: DoctorsSchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScheduleId,DoctorId,AvailableDate,StartTime,EndTime,TimeSlot,IsAvailable,SpecialDays,MaxAppointments,Notes")] DoctorsSchedule doctorsSchedule)
        {
            bool createStatus = false;
            if (ModelState.IsValid)
            {
                using (var http = new HttpClient())
                {
                    using (var repsone = await http.PostAsJsonAsync(Const.APIEndPoint + $"DoctorsSchedules", doctorsSchedule))
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

            ViewData["DoctorId"] = new SelectList(await GetDoctors(), "DoctorId", "FullName");
          
            return View(new ServiceRequest());

        }

        // GET: DoctorsSchedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var doctorSchedule = new DoctorsSchedule();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + $"DoctorsSchedules/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Status == Const.SUCCESS_READ_CODE)
                        {
                            doctorSchedule = JsonConvert.DeserializeObject<DoctorsSchedule>(result.Data.ToString());
                        }
                    }
                }
            }

            ViewData["DoctorId"] = new SelectList(await GetDoctors(), "DoctorId", "FullName");
           

            return View(doctorSchedule);
        }

        // POST: DoctorsSchedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScheduleId,DoctorId,AvailableDate,StartTime,EndTime,TimeSlot,IsAvailable,SpecialDays,MaxAppointments,Notes")] DoctorsSchedule doctorsSchedule)
        {
            bool updateStatus = false;
            if (id != doctorsSchedule.ScheduleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var http = new HttpClient())
                {
                    using (var repsone = await http.PostAsJsonAsync(Const.APIEndPoint + $"DoctorsSchedules", doctorsSchedule))
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

            ViewData["DoctorId"] = new SelectList(await GetDoctors(), "DoctorId", "FullName");
           
            return View(doctorsSchedule);
        }

        // GET: DoctorsSchedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var respone = await httpClient.GetAsync(Const.APIEndPoint + "Doctors/" + id))
                {
                    if (respone.IsSuccessStatusCode)
                    {
                        var content = await respone.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<Doctor>(result.Data.ToString());
                            return View(data);
                        }

                    }
                }
                return View(new Doctor());
            }
        }

        // POST: DoctorsSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool deleteStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync(Const.APIEndPoint + "Doctors/" + id))
                    {
                        if (response.IsSuccessStatusCode)
                        {

                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                            if (result != null && result.Status == Const.SUCCESS_DELETE_CODE)
                            {
                                deleteStatus = true;
                            }
                            else
                            {
                                deleteStatus = false;
                            }
                        }
                    }
                }
            }
            if (deleteStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Delete));
            }
        }

        private bool DoctorsScheduleExists(int id)
        {
            return _context.DoctorsSchedules.Any(e => e.ScheduleId == id);
        }

        private async Task<IList<Doctor>?> GetDoctors()
        {
            using (var http = new HttpClient())
            {
                var response = await http.GetAsync(Const.APIEndPoint + "Doctors");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                    if (result != null && result.Status == Const.SUCCESS_READ_CODE)
                    {
                        var data = JsonConvert.DeserializeObject<IList<Doctor>>(result.Data.ToString());
                        return data;
                    }
                }
            }

            return new List<Doctor>();
        }
    }
}
