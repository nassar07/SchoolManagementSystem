using AutoMapper;
using School.Application.DTOs;
using School.Application.DTOs.Attendance;
using School.Application.DTOs.Department;
using School.Application.Services.Interfaces;
using School.Domain.Entities;
using School.Domain.Interfaces;
using School.Domain.Interfaces.AttendanceSpecifies;
using School.Domain.Interfaces.Authentication;

namespace School.Application.Services.Implementation
{
    public class AttendanceService(IGeneric<Attendance> attendanceInterface,
        IMapper mapper,
        IRoleManagement role,
        IAttendance attendanceSpecifies,
        IGeneric<Class> classInterface) : IAttendanceService
    {
        public async Task<ServiceResponse<List<AttendanceDto>>> GetAttendanceByClassIdAsync(Guid classId)
        {
            var classExists = await classInterface.GetByIdAsync(classId);
            if (classExists == null)
            {
                return new ServiceResponse<List<AttendanceDto>>
                {
                    Data = null,
                    Success = false,
                    Message = "Class not found."
                };

            }
            var attendances = await attendanceSpecifies.GetAttendanceByClassIdAsync(classId);
            var attendanceDtos = mapper.Map<List<AttendanceDto>>(attendances);
            return new ServiceResponse<List<AttendanceDto>>
            {
                Data = attendanceDtos,
                Success = true,
                Message = "Attendance records retrieved successfully."
            };
        }


        public async Task<ServiceResponse> MarkAttendanceAsync(MarkAttendanceDto markAttendanceDto)
        {
            var classExists = await classInterface.GetByIdAsync(markAttendanceDto.ClassId);
            if (classExists == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Class not found."
                };
            }
            var IsTeacherRole = await role.GetRoleByUserId(markAttendanceDto.MarkedByTeacherId);
            var IsStudentRole = await role.GetRoleByUserId(markAttendanceDto.StudentId);
            if (IsTeacherRole != "Teacher")
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Must Marked By Teacher."
                };
            }
            if (IsStudentRole != "Student")
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Attendance must be marked for a Student."
                };
            }
            bool isStatusValid = markAttendanceDto.Status == "Present" || markAttendanceDto.Status == "Absent" || markAttendanceDto.Status == "Late";
            if (!isStatusValid)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Invalid attendance status. Must be 'Present', 'Absent', or 'Late'."
                };
            }
            var attendance = mapper.Map<MarkAttendanceDto, Attendance>(markAttendanceDto);
            var AddedAttendanceId = await attendanceInterface.AddAsync(attendance);
            if (AddedAttendanceId == Guid.Empty)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Failed to mark attendance."
                };
            }
            return new ServiceResponse
            {
                Success = true,
                Message = "Attendance marked successfully."
            };
        }
    }
}
