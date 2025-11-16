using AutoMapper;
using School.Application.DTOs;
using School.Application.DTOs.Course;
using School.Application.DTOs.Department;
using School.Application.Services.Interfaces;
using School.Domain.Entities;
using School.Domain.Interfaces;
using School.Domain.Interfaces.CourseSpecifies;

namespace School.Application.Services.Implementation
{
    public class CourseService(IGeneric<Course> courseInterface , IMapper mapper , ICourse courseSpecifies , IGeneric<Department> departmentInterface) : ICourseService
    {
        public async Task<ServiceResponse> CreateCourseAsync(CreateCourseDto createCourseDto)
        {
            if (!await courseSpecifies.IsCourseCodeUniqueAsync(createCourseDto.Code))
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Course Code must be unique."
                };
            }
            var department = mapper.Map<Course>(createCourseDto);
            var IsDepartmentExist = await departmentInterface.GetByIdAsync(createCourseDto.DepartmentId);
            if (IsDepartmentExist == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Department does not exist."
                };
            }
            var AddedCoursetId = await courseInterface.AddAsync(department);
            return new ServiceResponse
            {
                Success = true,
                Message = $"Course created successfully with ID: {AddedCoursetId}"
            };
        }

        public async Task<ServiceResponse> DeleteCourseAsync(Guid id)
        {
            var result = await courseInterface.DeleteAsync(id);
            return result > 0
                ? new ServiceResponse { Success = true, Message = "Course deleted successfully." }
                : new ServiceResponse { Success = false, Message = "Course deletion failed." };
        }

        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await courseInterface.GetAllAsync();
            var courseDtos = mapper.Map<IEnumerable<CourseDto>>(courses);
            return courseDtos;
        }

        public async Task<CourseDto> GetCourseByIdAsync(Guid id)
        {
            var course = await courseInterface.GetByIdAsync(id);
            var courseDto = mapper.Map<CourseDto>(course);
            return courseDto;
        }

        public async Task<ServiceResponse> UpdateCourseAsync(Guid id, UpdateCourseDto updateCoursetDto)
        {
            var existingCourse = await courseInterface.GetByIdAsync(id);
            if (existingCourse == null)
            {
                return new ServiceResponse { Success = false, Message = "NotFound" };
            }
            if (existingCourse.Code != updateCoursetDto.Code)
            {
                if (!await courseSpecifies.IsCourseCodeUniqueAsync(updateCoursetDto.Code))
                {
                    return new ServiceResponse
                    {
                        Success = false,
                        Message = "Course Code must be unique."
                    };
                }
            }
            var IsDepartmentExist = await departmentInterface.GetByIdAsync(updateCoursetDto.DepartmentId);
            if (IsDepartmentExist == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Department does not exist."
                };
            }
            mapper.Map(updateCoursetDto, existingCourse);
            var result = await courseInterface.UpdateAsync(existingCourse);
            return result > 0 ? new ServiceResponse { Success = true, Message = "Course Updated successfully." } : new ServiceResponse { Success = false, Message = "Failled " };
        }
    }
}
