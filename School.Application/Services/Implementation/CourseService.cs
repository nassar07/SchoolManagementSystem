using School.Application.DTOs;
using School.Application.DTOs.Course;
using School.Application.Services.Interfaces;

namespace School.Application.Services.Implementation
{
    public class CourseService : ICourseService
    {
        public Task<ServiceResponse> CreateCourseAsync(CreateCourseDto createCourseDto)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> DeleteDepartmentAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CourseDto> GetCourseByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> UpdateDepartmentAsync(Guid id, UpdateCourseDto updateCoursetDto)
        {
            throw new NotImplementedException();
        }
    }
}
