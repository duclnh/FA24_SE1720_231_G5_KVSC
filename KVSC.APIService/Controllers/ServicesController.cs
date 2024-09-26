using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KVSC.Data.Models;
using KVSC.Service.Base;
using KVSC.Services.Services;

namespace KVSC.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private IDSService _dSService;

        public ServicesController(IDSService dSService)
        {
            _dSService = dSService;
        }


        [HttpGet]
        public async Task<IBusinessResult> GetService()
        {
            return await _dSService.GetAll();
        }

        // GET: api/ServiceCategories/5
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetService(int id)
        {
            var serviceCategory = await _dSService.GetById(id);

            if (serviceCategory == null)
            {
                return (IBusinessResult)NotFound();
            }

            return serviceCategory;
        }


        [HttpPost]
        public async Task<IBusinessResult> PostServiceCategory([FromBody] Data.Models.Service service)
        {
            /*
             * cấu trúc truyền dữ liệu để tạo mới cùng voi bang phu
                {
                  "serviceName": "tam",
                  "description": "tam",
                  "basePrice": 0,
                  "homeVisitFee": 0,
                  "duration": 0,
                  "availability": true,
                  "isActive": true,
                  "category": {
                    "categoryId": 2,
                    "categoryName": "binh".
                  }
                }
             
            cấu trúc truyền dữ liệu để update và tạo mới
        {
            "serviceId": 1, //có id là update 
            "serviceName": "Koi Fish Grooming",
            "description": "Cham sóc, v? sinh cho cá Koi",
            "basePrice": 500000,
            "homeVisitFee": 100000,
            "duration": 60,
            "categoryId": 2,
            "availability": true,
            "createDate": "2024-09-25T03:23:12.963",
            "lastUpdate": "2024-09-25T03:23:12.963",
            "isActive": true
        }


             */

            return await _dSService.Save(service);
        }

        // DELETE: api/ServiceCategories/5
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteServiceCategory(int id)
        {


            return await _dSService.DeleteById(id);
        }
    }
}
