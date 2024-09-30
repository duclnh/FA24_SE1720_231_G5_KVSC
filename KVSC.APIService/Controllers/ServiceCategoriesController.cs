using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KVSC.Data.Models;
using KVSC.Service.Base;
using KVSC.Service.Services;

namespace KVSC.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceCategoriesController : ControllerBase
    {
        //private readonly FA24_SE1720_231_G5_KVSCContext _context;
        private readonly ICategoryService _categoryService;

        public ServiceCategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/ServiceCategories
        [HttpGet]
        public async Task<IBusinessResult> GetServiceCategories()
        {
            return await _categoryService.GetAll();
        }

        // GET: api/ServiceCategories/5
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetServiceCategory(int id)
        {
            var serviceCategory = await _categoryService.GetById(id);

            if (serviceCategory == null)
            {
                return (IBusinessResult)NotFound();
            }

            return serviceCategory;
        }

        // PUT: api/ServiceCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutServiceCategory(int id, ServiceCategory serviceCategory)
        {
            if (id != serviceCategory.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(serviceCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        // POST: api/ServiceCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IBusinessResult> PostServiceCategory(ServiceCategory serviceCategory)
        {
            return await _categoryService.Save(serviceCategory);
            /*_context.ServiceCategories.Add(serviceCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetServiceCategory", new { id = serviceCategory.CategoryId }, serviceCategory);*/
        }

        // DELETE: api/ServiceCategories/5
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteServiceCategory(int id)
        {
            /* var serviceCategory = await _categoryService.GetById(id);
             if (serviceCategory == null)
             {
                 return (IBusinessResult)NotFound();
             }

             _context.ServiceCategories.Remove(serviceCategory);
             await _context.SaveChangesAsync();

             return NoContent()*/
            ;

            return await _categoryService.DeleteById(id);
        }

        /*private bool ServiceCategoryExists(int id)
        {
            return _context.ServiceCategories.Any(e => e.CategoryId == id);
        }*/
    }
}
