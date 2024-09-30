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
using System.Net.Http;

namespace KVSC.MVCWebApp.Controllers
{
    public class ServicesController : Controller
    {
        /*private readonly FA24_SE1720_231_G5_KVSCContext _context;

        public ServicesController(FA24_SE1720_231_G5_KVSCContext context)
        {
            _context = context;
        }*/

        // GET: Services
        public async Task<IActionResult> Index()
        {

            using (var httpClient = new HttpClient())
            {
                using (var respone = await httpClient.GetAsync(Const.APIEndPoint + "Services"))
                {
                    if (respone.IsSuccessStatusCode)
                    {
                        var content = await respone.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<Data.Models.Service>>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }

            return View(new List<Data.Models.Service>());
            /*  var fA24_SE1720_231_G5_KVSCContext = _context.Services.Include(s => s.Category);
              return View(await fA24_SE1720_231_G5_KVSCContext.ToListAsync());*/
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            using (var httpClient = new HttpClient())
            {
                using (var respone = await httpClient.GetAsync(Const.APIEndPoint + "Services/" +id))
                {
                    if (respone.IsSuccessStatusCode)
                    {
                        var content = await respone.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<Data.Models.Service>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }

            return View(new Data.Models.Service());
            /*if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.ServiceId == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);*/
        }

        // GET: Services/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await this.GetServiceCategory(), "CategoryId", "CategoryName");
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceId,ServiceName,Description,BasePrice,HomeVisitFee,Duration,CategoryId,Availability,CreateDate,LastUpdate,IsActive")] Data.Models.Service service)
        {
            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var respone = await httpClient.PostAsJsonAsync(Const.APIEndPoint + "Services/",service))
                    {
                        if (respone.IsSuccessStatusCode)
                        {
                            var content = await respone.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Status == Const.SUCCESS_CREATE_CODE)
                            {
                                saveStatus = true;
                            }
                            else
                            {
                                saveStatus = false;
                            }
                        }
                    }
                }
            }
            if (saveStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["CategoryId"] = new SelectList(await this.GetServiceCategory(), "CategoryId", "CategoryName",service.CategoryId );
                return View();
            }
            
        }


        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var service = new Data.Models.Service();
            using (var httpClient = new HttpClient())
            {
                using (var respone = await httpClient.GetAsync(Const.APIEndPoint + "Services/" + id))
                {
                    if (respone.IsSuccessStatusCode)
                    {
                        var content = await respone.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            service = JsonConvert.DeserializeObject<Data.Models.Service>(result.Data.ToString());

                        }
                    }
                }
            }

                ViewData["CategoryId"] = new SelectList(await this.GetServiceCategory(), "CategoryId", "CategoryName", service.CategoryId);
                return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceId,ServiceName,Description,BasePrice,HomeVisitFee,Duration,CategoryId,Availability,CreateDate,LastUpdate,IsActive")] Data.Models.Service service)
        {
            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var respone = await httpClient.PutAsJsonAsync(Const.APIEndPoint + "Services/", service))
                    {
                        if (respone.IsSuccessStatusCode)
                        {
                            var content = await respone.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Status == Const.SUCCESS_UPDATE_CODE)
                            {
                                saveStatus = true;
                            }
                            else
                            {
                                saveStatus = false;
                            }
                        }
                    }
                }
            }
            if (saveStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["CategoryId"] = new SelectList(await this.GetServiceCategory(), "CategoryId", "CategoryName", service.CategoryId);
                return View(service);
            }
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            using (var httpClient = new HttpClient())
            {
                using (var respone = await httpClient.GetAsync(Const.APIEndPoint + "Services/" + id))
                {
                    if (respone.IsSuccessStatusCode)
                    {
                        var content = await respone.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<Data.Models.Service>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
                return View(new Data.Models.Service());
                /* if (id == null)
                 {
                     return NotFound();
                 }

                 var service = await _context.Services
                     .Include(s => s.Category)
                     .FirstOrDefaultAsync(m => m.ServiceId == id);
                 if (service == null)
                 {
                     return NotFound();
                 }

                 return View(service);*/
            }
        }
            // POST: Services/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                bool deleteStatus = false;

                if (ModelState.IsValid) {

                    using (var httpClient = new HttpClient())
                    {
                        using (var respone = await httpClient.DeleteAsync(Const.APIEndPoint + "Services/"+id))
                        {
                            if (respone.IsSuccessStatusCode)
                            {
                                var content = await respone.Content.ReadAsStringAsync();
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
                if(deleteStatus)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(Delete));
                }
              
            }

        public async Task<List<ServiceCategory>> GetServiceCategory()
        {
            var categorys = new List<ServiceCategory>();

            using (var httpClient = new HttpClient())
            {
                using (var respone = await httpClient.GetAsync(Const.APIEndPoint + "ServiceCategories"))
                {
                    if (respone.IsSuccessStatusCode)
                    {
                        var content = await respone.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            categorys = JsonConvert.DeserializeObject<List<Data.Models.ServiceCategory>>(result.Data.ToString());
 
                        }
                    }
                }
            }

            return categorys ;
            /*  var fA24_SE1720_231_G5_KVSCContext = _context.Services.Include(s => s.Category);
              return View(await fA24_SE1720_231_G5_KVSCContext.ToListAsync());*/
        }
    }
    }

