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
    public class PondRepository : GenericRepository<Pond>
    {
        public PondRepository()
        {

        }

        public PondRepository(FA24_SE1720_231_G5_KVSCContext context) => _context = context;

        public async Task<IList<Pond>> GetAllPondAsync()
        {
            return await _context.Ponds.AsNoTracking().OrderBy(x => x.PondId)
                .ToListAsync(); 
        }

        public async Task<Pond?> GetPondByIdAsync(int id)
        {
            return await _context.Ponds.AsNoTracking().OrderBy(x => x.PondId)
                .FirstOrDefaultAsync(x => x.PondId == id);
        }
    }
}
