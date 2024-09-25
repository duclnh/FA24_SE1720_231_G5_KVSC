using KVSC.Data.Base;
using KVSC.Data.Models;
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
    }
}
