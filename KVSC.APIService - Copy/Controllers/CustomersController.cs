using Microsoft.AspNetCore.Mvc;
using KVSC.Data.Models;
using KVSC.Service.Service;
using KVSC.Service.Base;

namespace KVSC.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<IBusinessResult> GetCustomers()
        {
            return await _customerService.GetAll();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetCustomer(int id)
        {
            return await _customerService.GetById(id);
        }

        //// PUT: api/Customers/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IBusinessResult> PutCustomer(Customer customer)
        {
            return await _customerService.Save(customer);
        }

        //// POST: api/Customers
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IBusinessResult> PostCustomer(Customer customer)
        {
            return await _customerService.Save(customer);
        }

        //// DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteCustomer(int id)
        {
            return await _customerService.Delete(id);
        }

    }
}
