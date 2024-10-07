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
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        // GET: api/Doctors
        [HttpGet]
        public async Task<IBusinessResult> GetDoctors()
        {
            return await _doctorService.GetAll();

        }

        // GET: api/Doctors/5
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetDoctor(int id)
        {
            return await _doctorService.GetById(id);


        }



        // POST: api/Doctors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IBusinessResult> PostDoctor([FromBody] Doctor doctor)
        {
            return await _doctorService.Save(doctor);



            /*
             {
  
    {
  
    "doctorId": 0,
    "fullName": "test",
    "dateOfBirth": "1980-05-15",
    "phoneNumber":"00000000",
    "email": "john.smith@example.com",
    "address": "123 Medical Center Blvd, Healthville, HV 12345",
    "specialization": "Ichthyology",
    "yearsOfExperience": 15,
    "ratingAverage": 4.8,
    "status": "Active",
    "profilePicture": "https://example.com/dr-smith-profile.jpg"
  }

  }

             */
        }

        // DELETE: api/Doctors/5
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteDoctor(int id)
        {
            return await _doctorService.DeleteById(id);
        }
    }
}
