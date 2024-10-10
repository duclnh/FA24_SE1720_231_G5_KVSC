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
    public class DoctorSheduleRepository : GenericRepository<DoctorsSchedule>
    {
        public DoctorSheduleRepository() { }
        public DoctorSheduleRepository(FA24_SE1720_231_G5_KVSCContext context) : base(context) => _context = context;

        public async Task<IList<DoctorsSchedule>> GetAllDoctorScheduleAsync()
        {
            return await _context.DoctorsSchedules.AsNoTracking().OrderByDescending(x => x.ScheduleId)
                .Include(x => x.Doctor)
                .ToListAsync();
        }

        public async Task<DoctorsSchedule?> GetDoctorScheduleByIdAsync(int id)
        {
            return await _context.DoctorsSchedules.AsNoTracking().OrderByDescending(x => x.ScheduleId)
                .Include(x => x.Doctor)
                .FirstOrDefaultAsync(x => x.ScheduleId == id);
        }

    }
}
