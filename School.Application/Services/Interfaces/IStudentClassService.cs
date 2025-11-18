using School.Application.DTOs;
using School.Application.DTOs.StudentClass;

namespace School.Application.Services.Interfaces
{
    public interface IStudentClassService
    {
        Task<ServiceResponse<IEnumerable<StudentClassDto>>> GetClassesBySudetId(Guid StudentId);
    }
}
