using School.Application.DTOs;
using School.Application.DTOs.Course;

namespace School.Application.Services.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<CourseDto> GetCourseByIdAsync(Guid id);
        Task<ServiceResponse> CreateCourseAsync(CreateCourseDto createCourseDto);
        Task<ServiceResponse> UpdateCourseAsync(Guid id, UpdateCourseDto updateCoursetDto);
        Task<ServiceResponse> DeleteCourseAsync(Guid id);
    }
}
