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
    public class FishController : ControllerBase
    {
        private readonly IFishService _fishService;

        public FishController(IFishService fishService)
        {
            _fishService = fishService;
        }

        // GET: api/Fishs
        [HttpGet]
        public async Task<IBusinessResult> GetFish()
        {
            return await _fishService.GetAll();
        }

        // GET: api/Fishs/5
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetFish(int id)
        {
            return await _fishService.GetById(id);
        }

        // PUT: api/Fishs/5
        [HttpPut("{id}")]
        public async Task<IBusinessResult> PutFish(Fish fish)
        {
            return await _fishService.Save(fish);
        }

        // POST: api/Fishs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IBusinessResult> PostFish(Fish fish)
        {
            return await _fishService.Save(fish);
        }

        // DELETE: api/Fishs/5
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteFish(int id)
        {
            return await _fishService.DeleteById(id);
        }
    }
}
