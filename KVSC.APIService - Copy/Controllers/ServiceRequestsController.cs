using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KVSC.Data.Models;
using KVSC.Service.Service;
using KVSC.Service.Base;

namespace KVSC.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceRequestsController : ControllerBase
    {
        private readonly IServiceRequestService _serviceRequestService;

        public ServiceRequestsController(IServiceRequestService serviceRequestService)
        {
            _serviceRequestService = serviceRequestService;
        }

        // GET: api/ServiceRequests
        [HttpGet]
        public async Task<IBusinessResult> GetServiceRequests()
        {
            return await _serviceRequestService.GetAll();
        }

        // GET: api/ServiceRequests/5
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetServiceRequest(int id)
        {
            return await _serviceRequestService.GetById(id);
        }

        // PUT: api/ServiceRequests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IBusinessResult> PutServiceRequest(ServiceRequest serviceRequest)
        {
            return await _serviceRequestService.Save(serviceRequest);
        }

        // POST: api/ServiceRequests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IBusinessResult> PostServiceRequest(ServiceRequest serviceRequest)
        {
            return await _serviceRequestService.Save(serviceRequest);
        }

        // DELETE: api/ServiceRequests/5
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteServiceRequest(int id)
        {
            return await _serviceRequestService.Delete(id);
        }

    }
}
