using KVSC.Data.Base;
using KVSC.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVSC.Data.Repository
{
    public class ServiceRepository : GenericRepository<Service>
    {
        public ServiceRepository() { }
        public ServiceRepository(FA24_SE1720_231_G5_KVSCContext context) : base(context)=> _context = context;

        public async Task<List<Service>> GetListService()
        {
            return await _context.Services.Include(x => x.Category).ToListAsync();
        }

        public async Task<List<Service>> FindBy(string categoryName, string serviceName, string status)
        {
            // Use more precise filtering logic
            return await _context.Services
                        .Where(x =>
                            (string.IsNullOrEmpty(serviceName) || x.ServiceName.Contains(serviceName)) &&
                            (string.IsNullOrEmpty(categoryName) || x.Category.CategoryName.Contains(categoryName)) &&
                            (string.IsNullOrEmpty(status) || x.Description.Contains(status)))
                        .Include(x => x.Category)
                        .ToListAsync();
        }


    }
}
