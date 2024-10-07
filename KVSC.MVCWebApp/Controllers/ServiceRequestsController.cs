using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KVSC.Data.Models;
using KVSC.Common;
using Newtonsoft.Json;
using KVSC.Service.Base;
using System.Data;


namespace KVSC.MVCWebApp.Controllers
{
    public class ServiceRequestsController : Controller
    {

        public ServiceRequestsController()
        {
        }

        //GET: ServiceRequests
        public async Task<IActionResult> Index()
        {
            using (var http = new HttpClient())
            {
                using (var response = await http.GetAsync(Const.APIEndPoint + "ServiceRequests"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content.ToString());
                        if (result != null && result.Status == Const.SUCCESS_READ_CODE)
                        {
                            var data = JsonConvert.DeserializeObject<List<ServiceRequest>>(result.Data.ToString());
                            return View(data);
                        }

                    }
                }
            }
            return View(new List<ServiceRequest>());
        }

        //GET: ServiceRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            using (var http = new HttpClient())
            {
                using (var repsone = await http.GetAsync(Const.APIEndPoint + $"ServiceRequests/{id}"))
                {
                    if (repsone.IsSuccessStatusCode)
                    {
                        var content = await repsone.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Status == Const.SUCCESS_READ_CODE)
                        {
                            var data = JsonConvert.DeserializeObject<ServiceRequest>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new ServiceRequest());
        }

        // GET: ServiceRequests/Create
        public async Task<IActionResult> Create()
        {
            ViewData["AssignedDoctorId"] = new SelectList(await GetDoctors(), "DoctorId", "FullName");
            ViewData["CustomerId"] = new SelectList(await GetCustomers(), "CustomerId", "FullName");
            ViewData["ServiceId"] = new SelectList(await GetServices(), "ServiceId", "ServiceName");
            return View();
        }

        // POST: ServiceRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RequestId,CustomerId,ServiceId,RequestDate,RequestDetails,Status,AssignedDoctorId,CompletionDate,TotalAmount,Feedback,Notes")] ServiceRequest serviceRequest)
        {
            bool createStatus = false;
            if (ModelState.IsValid)
            {
                using (var http = new HttpClient())
                {
                    using (var repsone = await http.PostAsJsonAsync(Const.APIEndPoint + $"ServiceRequests", serviceRequest))
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

            ViewData["AssignedDoctorId"] = new SelectList(await GetDoctors(), "DoctorId", "FullName");
            ViewData["CustomerId"] = new SelectList(await GetCustomers(), "CustomerId", "FullName");
            ViewData["ServiceId"] = new SelectList(await GetServices(), "ServiceId", "ServiceName");
            return View(new ServiceRequest());
        }

        // GET: ServiceRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var serviceRequest = new ServiceRequest();

            using (var httpClient = new HttpClient())
            {
                using(var response = await httpClient.GetAsync(Const.APIEndPoint + $"ServiceRequests/{id}" ))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if(result != null && result.Status == Const.SUCCESS_READ_CODE)
                        {
                            serviceRequest = JsonConvert.DeserializeObject<ServiceRequest>(result.Data.ToString());
                        }
                    }
                }
            }

            ViewData["AssignedDoctorId"] = new SelectList(await GetDoctors(), "DoctorId", "FullName");
            ViewData["CustomerId"] = new SelectList(await GetCustomers(), "CustomerId", "FullName");
            ViewData["ServiceId"] = new SelectList(await GetServices(), "ServiceId", "ServiceName");

            return View(serviceRequest);
        }

        // POST: ServiceRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RequestId,CustomerId,ServiceId,RequestDate,RequestDetails,Status,AssignedDoctorId,CompletionDate,TotalAmount,Feedback,Notes")] ServiceRequest serviceRequest)
        {
            bool updateStatus = false;
            if (id != serviceRequest.RequestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var http = new HttpClient())
                {
                    using (var repsone = await http.PutAsJsonAsync(Const.APIEndPoint + $"ServiceRequests", serviceRequest))
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
            if(updateStatus)
                return RedirectToAction(nameof(Index));

            ViewData["AssignedDoctorId"] = new SelectList(await GetDoctors(), "DoctorId", "FullName");
            ViewData["CustomerId"] = new SelectList(await GetCustomers(), "CustomerId", "FullName");
            ViewData["ServiceId"] = new SelectList(await GetServices(), "ServiceId", "ServiceName");
            return View(serviceRequest);
        }

        // GET: ServiceRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            using (var http = new HttpClient())
            {
                using (var repsone = await http.GetAsync(Const.APIEndPoint + $"ServiceRequests/{id}"))
                {
                    if (repsone.IsSuccessStatusCode)
                    {
                        var content = await repsone.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Status == Const.SUCCESS_READ_CODE)
                        {
                            var data = JsonConvert.DeserializeObject<ServiceRequest>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new ServiceRequest());
        }

        // POST: ServiceRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool deleteStatus = false;
            using (var http = new HttpClient())
            {
                using (var repsone = await http.DeleteAsync(Const.APIEndPoint + $"ServiceRequests/{id}"))
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

        private async Task<IList<Doctor>?> GetDoctors()
        {
            using (var http = new HttpClient())
            {
                var response = await http.GetAsync(Const.APIEndPoint + "Doctors");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                    if(result != null && result.Status == Const.SUCCESS_READ_CODE)
                    {
                        var data = JsonConvert.DeserializeObject<IList<Doctor>>(result.Data.ToString());
                        return data;
                    }
                }
            }

            return new List<Doctor>();
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

        private async Task<IList<Data.Models.Service>?> GetServices()
        {
            using (var http = new HttpClient())
            {
                var response = await http.GetAsync(Const.APIEndPoint + "Services");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                    if (result != null && result.Status == Const.SUCCESS_READ_CODE)
                    {
                        var data = JsonConvert.DeserializeObject<IList<Data.Models.Service>>(result.Data.ToString());
                        return data;
                    }
                }
            }

            return new List<Data.Models.Service>();
        }
    }
}
