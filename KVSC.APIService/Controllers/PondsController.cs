using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KVSC.Data.Models;
using KVSC.Services.Services;
using KVSC.Service.Base;

namespace KVSC.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PondsController : ControllerBase
    {
        private readonly IPondService _pondService;

        public PondsController(IPondService pondService)
        {
            _pondService = pondService;
        }

        // GET: api/Ponds
        [HttpGet]
        public async Task<IBusinessResult> GetPond()
        {
            return await _pondService.GetAll();
        }

        // GET: api/Ponds/5
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetPond(int id)
        {
            return await _pondService.GetById(id);
        }

        // PUT: api/Ponds/5
        [HttpPut("{id}")]
        public async Task<IBusinessResult> PutPond(Pond pond)
        {
            return await _pondService.Save(pond);
        }

        // POST: api/Ponds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IBusinessResult> PostPond(Pond pond)
        {
            return await _pondService.Save(pond);
        }

        // DELETE: api/Ponds/5
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeletePond(int id)
        {
            return await _pondService.DeleteById(id);
        }
    }
}
