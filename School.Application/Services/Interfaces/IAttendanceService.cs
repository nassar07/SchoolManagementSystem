using School.Application.DTOs;
using School.Application.DTOs.Attendance;

namespace School.Application.Services.Interfaces
{
    public interface IAttendanceService
    {
        Task<ServiceResponse> MarkAttendanceAsync(MarkAttendanceDto markAttendanceDto);
        Task<ServiceResponse<List<AttendanceDto>>> GetAttendanceByClassIdAsync(Guid classId);
    }
}
