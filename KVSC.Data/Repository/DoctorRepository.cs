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
    public class DoctorRepository : GenericRepository<Doctor>
    {
        public DoctorRepository() { }
        public DoctorRepository(FA24_SE1720_231_G5_KVSCContext context) : base(context) => _context = context;

        public async Task<List<Doctor>> GetAllDoctor()
        {
            return await _context.Doctors.Include(x => x.DoctorsSchedules).ToListAsync();
        }
    }
}
