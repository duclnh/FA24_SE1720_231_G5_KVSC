using KVSC.Data.Base;
using KVSC.Data.Models;

namespace KVSC.Data.Repository
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository()
        {
            
        }

        public CustomerRepository(FA24_SE1720_231_G5_KVSCContext context) => _context = context;
    }
}
