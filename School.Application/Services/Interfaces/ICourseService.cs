using School.Application.DTOs;
using School.Application.DTOs.Course;
using School.Application.DTOs.Department;

namespace School.Application.Services.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<CourseDto> GetCourseByIdAsync(Guid id);
        Task<ServiceResponse> CreateCourseAsync(CreateCourseDto createCourseDto);
        Task<ServiceResponse> UpdateDepartmentAsync(Guid id, UpdateCourseDto updateCoursetDto);
        Task<ServiceResponse> DeleteDepartmentAsync(Guid id);
    }
}
