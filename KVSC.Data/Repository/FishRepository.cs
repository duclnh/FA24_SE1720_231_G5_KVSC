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
    public class FishRepository : GenericRepository<Fish>
    {
        public FishRepository()
        {

        }

        public FishRepository(FA24_SE1720_231_G5_KVSCContext context) => _context = context;

        public async Task<IList<Fish>> GetAllFishAsync()
        {
            return await _context.Fish.AsNoTracking().OrderBy(x => x.FishId)
                .Include(x => x.Customer)
                                .Include(x => x.Pond)
                .ToListAsync(); 
        }

        public async Task<Fish?> GetFishByIdAsync(int id)
        {
            return await _context.Fish.AsNoTracking().OrderBy(x => x.FishId)
                .Include(x => x.Customer)
                .Include(x => x.Pond)
                .FirstOrDefaultAsync(x => x.FishId == id);
        }
    }
}
