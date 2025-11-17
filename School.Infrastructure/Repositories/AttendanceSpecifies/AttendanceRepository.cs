using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;
using School.Domain.Interfaces.AttendanceSpecifies;
using School.Infrastructure.Data;

namespace School.Infrastructure.Repositories.AttendanceSpecifies
{
    public class AttendanceRepository(AppDbContext context) : IAttendance
    {        
        public async Task<IEnumerable<Attendance>> GetAttendanceByClassIdAsync(Guid classId)
        {
            var attendances = await context.Attendances
                .Where(a => a.ClassId == classId)
                .AsNoTracking()
                .ToListAsync();
            return attendances;
        }
    }
}
