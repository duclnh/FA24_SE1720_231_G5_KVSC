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
    public class ServiceRequestRepository : GenericRepository<ServiceRequest>
    {
        public ServiceRequestRepository()
        {

        }

        public ServiceRequestRepository(FA24_SE1720_231_G5_KVSCContext context) => _context = context;

        public async Task<IList<ServiceRequest>> GetAllServiceRequestsAsync()
        {
            return await _context.ServiceRequests.AsNoTracking().OrderByDescending(x => x.RequestId)
                .Include(x => x.AssignedDoctor)
                .Include(x => x.Service)
                .Include(x => x.Customer)
                .ToListAsync(); 
        }

        public async Task<ServiceRequest?> GetServiceRequestByIdAsync(int id)
        {
            return await _context.ServiceRequests.AsNoTracking().OrderByDescending(x => x.RequestId)
                .Include(x => x.AssignedDoctor)
                .Include(x => x.Service)
                .Include(x => x.Customer)
                .FirstOrDefaultAsync(x => x.RequestId == id);
        }
    }
}
