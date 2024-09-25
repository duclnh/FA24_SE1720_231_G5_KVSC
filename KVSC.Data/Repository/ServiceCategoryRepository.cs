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
    public class ServiceCategoryRepository : GenericRepository<ServiceCategoryRepository>
    {
        public ServiceCategoryRepository() { }
        public ServiceCategoryRepository(FA24_SE1720_231_G5_KVSCContext context) : base(context) => _context = context;

        public async Task<List<ServiceCategory>> GetListAsync()
        {
            return await _context.ServiceCategories
                         .Include(x => x.Services) // Include bảng liên quan (nếu có)
                         .ToListAsync();
        }

        public async Task<int> CreateAsync(ServiceCategory serviceCategory)
        {
            _context.ServiceCategories.Add(serviceCategory);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(ServiceCategory serviceCategory)
        {
            _context.ServiceCategories.Update(serviceCategory);
            return await _context.SaveChangesAsync();
        }
    }
}
