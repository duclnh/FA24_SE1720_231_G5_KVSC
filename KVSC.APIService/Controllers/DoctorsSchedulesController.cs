using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KVSC.Data.Models;
using KVSC.Service.Base;
using KVSC.Service.Service;

namespace KVSC.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsSchedulesController : ControllerBase
    {
        private readonly IDoctorScheduleService _doctorscheduleService;

        public DoctorsSchedulesController(IDoctorScheduleService doctorScheduleService)
        {
            _doctorscheduleService = doctorScheduleService;
        }

        // GET: api/DoctorsSchedules
        [HttpGet]
        public async Task<IBusinessResult> GetDoctorsSchedules()
        {
            return await _doctorscheduleService.GetAll();
        }

        // GET: api/DoctorsSchedules/5
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetDoctorsSchedule(int id)
        {
            return await _doctorscheduleService.GetById(id);
        }

        // PUT: api/DoctorsSchedules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /* [HttpPut("{id}")]
         public async Task<IActionResult> PutDoctorsSchedule(int id, DoctorsSchedule doctorsSchedule)
         {
             if (id != doctorsSchedule.ScheduleId)
             {
                 return BadRequest();
             }

             _context.Entry(doctorsSchedule).State = EntityState.Modified;

             try
             {
                 await _context.SaveChangesAsync();
             }
             catch (DbUpdateConcurrencyException)
             {
                 if (!DoctorsScheduleExists(id))
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

        // POST: api/DoctorsSchedules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IBusinessResult> PostDoctorsSchedule(DoctorsSchedule doctorsSchedule)
        {
            return await _doctorscheduleService.Save(doctorsSchedule);
        }

        // DELETE: api/DoctorsSchedules/5
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteDoctorsSchedule(int id)
        {
            return await _doctorscheduleService.DeleteById(id);
        }

    }
}
