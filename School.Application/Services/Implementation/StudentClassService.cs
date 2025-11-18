using AutoMapper;
using School.Application.DTOs;
using School.Application.DTOs.StudentClass;
using School.Application.Services.Interfaces;
using School.Domain.Entities;
using School.Domain.Interfaces.Authentication;
using School.Domain.Interfaces.StudentClassSpecifies;

namespace School.Application.Services.Implementation
{
    public class StudentClassService(IStudentClass studentClassRepository, IMapper mapper, IRoleManagement role) : IStudentClassService
    {
        public async Task<ServiceResponse<IEnumerable<StudentClassDto>>> GetClassesBySudetId(Guid StudentId)
        {
            var IsStudent = await role.GetRoleByUserId(StudentId);
            if (IsStudent != "Student")
            {
                return new ServiceResponse<IEnumerable<StudentClassDto>>
                {
                    Data = null,
                    Success = false,
                    Message = "User is not a student."
                };
            }
            var studentClass = await studentClassRepository.GetStudentClassByStudentIdAsync(StudentId);
            if (studentClass == null)
            {
                return new ServiceResponse<IEnumerable<StudentClassDto>>
                {
                    Data = null,
                    Success = false,
                    Message = "Student is not assigned to any class."
                };
            }
            var studentClassDto = mapper.Map<IEnumerable<StudentClassDto>>(studentClass);
            return new ServiceResponse<IEnumerable<StudentClassDto>>
            {
                Success = true,
                Message = "Student class retrieved successfully.",
                Data = studentClassDto
            };

        }

    }
}
