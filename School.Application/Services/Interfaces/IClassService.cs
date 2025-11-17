using School.Application.DTOs;
using School.Application.DTOs.Class;
using School.Application.DTOs.StudentClass;

namespace School.Application.Services.Interfaces
{
    public interface IClassService
    {
        Task<IEnumerable<ClassDto>> GetAllClassesAsync();
        Task<ClassDto> GetClassByIdAsync(Guid id);
        Task<ServiceResponse> CreateClassAsync(CreateClassDto createClasstDto);
        Task<ServiceResponse> UpdateClassAsync(Guid id, UpdateClassDto updateClassDto);
        Task<ServiceResponse> DeleteClassAsync(Guid id);
        Task<ServiceResponse> DeactivateClassAsync(Guid id);
        Task<ServiceResponse> AssignStudentToClassAsync(EnrollStudentDto enrollStudentDto);
    }
}
