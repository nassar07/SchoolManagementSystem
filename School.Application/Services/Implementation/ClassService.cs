using AutoMapper;
using School.Application.DTOs;
using School.Application.DTOs.Class;
using School.Application.DTOs.StudentClass;
using School.Application.Services.Interfaces;
using School.Domain.Entities;
using School.Domain.Interfaces;
using School.Domain.Interfaces.Authentication;
using School.Domain.Interfaces.DepartmentSpecifies;

namespace School.Application.Services.Implementation
{
    public class ClassService(IGeneric<Class> classInterface ,
        IMapper mapper, 
        IClass classSpeciefies, 
        IRoleManagement role , 
        ICourseService courseService, 
        IGeneric<StudentClass> StudentClassInterface) : IClassService
    {
        public async Task<ServiceResponse> CreateClassAsync(CreateClassDto createClasstDto)
        {
            if (!await classSpeciefies.IsClassNameUniqueAsync(createClasstDto.Name))
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Class name must be unique."
                };
            }
            var isCourseExists = await courseService.GetCourseByIdAsync(createClasstDto.CourseId);
            if (isCourseExists == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Associated course does not exist."
                };
            }
            var userRole = await role.GetRoleByUserId(createClasstDto.TeacherId);
            if (userRole != "Teacher")
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Head of Class must have the role of Teacher."
                };
            }

            var NewClass = mapper.Map<Class>(createClasstDto);
            var AddedClassId = await classInterface.AddAsync(NewClass);
            return new ServiceResponse
            {
                Success = true,
                Message = $"Class created successfully with ID: {AddedClassId}"
            };
        }

        public async Task<ServiceResponse> DeactivateClassAsync(Guid id)
        {
            var result = await classSpeciefies.DeactiveClass(id);
            return result ? new ServiceResponse { Success = true, Message = "Class deactivated successfully." } 
            : new ServiceResponse { Success = false, Message = "Class deactivation failed." };
        }

        public async Task<ServiceResponse> DeleteClassAsync(Guid id)
        {
            var result = await classInterface.DeleteAsync(id);
            return result > 0
                ? new ServiceResponse { Success = true, Message = "Class deleted successfully." }
                : new ServiceResponse { Success = false, Message = "Class deletion failed." };
        }

        public async Task<IEnumerable<ClassDto>> GetAllClassesAsync()
        {
            var classes = await classInterface.GetAllAsync();
            var classDtos = mapper.Map<IEnumerable<ClassDto>>(classes);
            return classDtos;
        }

        public async Task<ClassDto> GetClassByIdAsync(Guid id)
        {
            var Class = await classInterface.GetByIdAsync(id);
            var ClassDto = mapper.Map<ClassDto>(Class);
            return ClassDto;
        }

        public async Task<ServiceResponse> UpdateClassAsync(Guid id, UpdateClassDto updateClassDto)
        {
            var existingClass = await classInterface.GetByIdAsync(id);
            if (existingClass == null)
            {
                return new ServiceResponse { Success = false, Message = "NotFound" };
            }
            if (existingClass.Name != updateClassDto.Name)
            {
                if (!await classSpeciefies.IsClassNameUniqueAsync(updateClassDto.Name))
                {
                    return new ServiceResponse
                    {
                        Success = false,
                        Message = "Class name must be unique."
                    };
                }
            }
            bool isHeadChanged = existingClass.TeacherId != updateClassDto.TeacherId;
            if (isHeadChanged)
            {
                var userRole = await role.GetRoleByUserId(updateClassDto.TeacherId);
                if (userRole != "Teacher")
                {
                    return new ServiceResponse
                    {
                        Success = false,
                        Message = "Head of Class must have the role of Teacher."
                    };
                }
            }
            bool isCourseChanged = existingClass.CourseId != updateClassDto.CourseId;
            if (isCourseChanged)
            {
                var isCourseExists = await courseService.GetCourseByIdAsync(updateClassDto.CourseId);
                if (isCourseExists == null)
                {
                    return new ServiceResponse
                    {
                        Success = false,
                        Message = "Associated course does not exist."
                    };
                }
            }
            mapper.Map(updateClassDto, existingClass);
            var result = await classInterface.UpdateAsync(existingClass);
            return result > 0 ? new ServiceResponse { Success = true, Message = "Class Updated successfully." } : new ServiceResponse { Success = false, Message = "Failled " };
        }
        public async Task<ServiceResponse> AssignStudentToClassAsync(EnrollStudentDto enrollStudentDto)
        {
            var IsClassExist = await classInterface.GetByIdAsync(enrollStudentDto.ClassId);
            if (IsClassExist == null)
                {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Class does not exist."
                };
            }
            var IsStudentExist = await role.GetRoleByUserId(enrollStudentDto.StudentId);
            if (IsStudentExist != "Student")
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "User is not a student."
                };
            }
            var NewStudentClass = mapper.Map<EnrollStudentDto, StudentClass>(enrollStudentDto);
            var AddedStudentClassId = await StudentClassInterface.AddAsync(NewStudentClass);
            return new ServiceResponse
            {
                Success = true,
                Message = $"Student enrolled successfully with Enrollment ID: {AddedStudentClassId}"
            };
        }


    }
}
