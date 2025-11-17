using AutoMapper;
using School.Application.DTOs.Attendance;
using School.Application.DTOs.Class;
using School.Application.DTOs.Course;
using School.Application.DTOs.Department;
using School.Application.DTOs.Identity;
using School.Application.DTOs.StudentClass;
using School.Domain.Entities;
using School.Domain.Entities.Identity;

namespace School.Application.Mappings
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<LoginUser, ApplicationUser>();
            CreateMap<CreateUserDto, ApplicationUser>();

            CreateMap<CreateDepartmentDto, Department>();
            CreateMap<DepartmentDto, Department>();
            CreateMap<UpdateDepartmentDto,Department>();
            CreateMap<Department, DepartmentDto>();


            CreateMap<CreateClassDto, Class>();
            CreateMap<ClassDto, Class>();
            CreateMap<UpdateClassDto, Class>();
            CreateMap<Class, ClassDto>();


            CreateMap<CreateCourseDto, Course>();
            CreateMap<CourseDto, Course>();
            CreateMap<UpdateCourseDto, Course>();
            CreateMap<Course, CourseDto>();


            CreateMap<EnrollStudentDto, StudentClass>();

            CreateMap<AttendanceDto, Attendance>();
            CreateMap<MarkAttendanceDto, Attendance>();
            CreateMap<Attendance, AttendanceDto>();






        }
    }
}
