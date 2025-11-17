using School.Domain.Entities;

namespace School.Domain.Interfaces.AttendanceSpecifies
{
    public interface IAttendance
    {
        Task<IEnumerable<Attendance>> GetAttendanceByClassIdAsync(Guid classId);
    }
}
