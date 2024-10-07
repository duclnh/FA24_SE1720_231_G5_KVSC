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

namespace KVSC.MVCWebApp.Controllers
{
    public class CustomersController : Controller
    {
        public CustomersController()
        {
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Customers"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<Customer>>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }

            return View(new List<Customer>());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + $"Customers/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<Customer>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }

            return View(new Customer());
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,FullName,PhoneNumber,Email,Address,DateOfBirth,Gender,JoinDate,MembershipType,TotalOrders")] Customer customer)
        {
            if(ModelState.IsValid)
            {
                bool createStatus = false;
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(Const.APIEndPoint + $"Customers", customer))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Status == Const.SUCCESS_CREATE_CODE)
                            {
                                createStatus = true;
                            }
                            else
                            {
                                createStatus = false;
                            }
                        }
                    }
                }

                if (!createStatus)
                    return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(Const.APIEndPoint + $"Customers/{id}"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Data != null)
                            {
                                var data = JsonConvert.DeserializeObject<Customer>(result.Data.ToString());
                                if (data == null)
                                {
                                    return NotFound();
                                }
                                return View(data);
                            }
                        }
                    }
                }
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }


            return View(new Customer());
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,FullName,PhoneNumber,Email,Address,DateOfBirth,Gender,JoinDate,MembershipType,TotalOrders")] Customer customer)
        {
            bool updateStatus = false;
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(Const.APIEndPoint + $"Customers", customer))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Data != null)
                            {
                                var data = JsonConvert.DeserializeObject<Customer>(result.Data.ToString());
                                if (result != null && result.Status == Const.SUCCESS_UPDATE_CODE)
                                {
                                    updateStatus = true;
                                }
                                else
                                {
                                    updateStatus = false;
                                }
                                return View(data);
                            }
                        }
                    }
                }
            }

            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(Const.APIEndPoint + $"Customers/{id}"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Data != null)
                            {
                                var data = JsonConvert.DeserializeObject<Customer>(result.Data.ToString());
                                if (data == null)
                                {
                                    return NotFound();
                                }
                                return View(data);
                            }
                        }
                    }
                }
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }


            return View(new Customer());
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool deleteStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync(Const.APIEndPoint + $"Customers/{id}"))
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

            return RedirectToAction(nameof(Delete));
        }
    }
}
