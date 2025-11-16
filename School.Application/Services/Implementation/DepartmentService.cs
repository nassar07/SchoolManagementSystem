using AutoMapper;
using School.Application.DTOs;
using School.Application.DTOs.Department;
using School.Application.Services.Interfaces;
using School.Domain.Entities;
using School.Domain.Interfaces;
using School.Domain.Interfaces.Authentication;
using School.Domain.Interfaces.DepartmentSpecifies;

namespace School.Application.Services.Implementation
{
    public class DepartmentService(IGeneric<Department> departmentInterface, IMapper mapper, IRoleManagement role, IDepartment departmentSpecifies) : IDepartmentService
    {
        public async Task<ServiceResponse> CreateDepartmentAsync(CreateDepartmentDto createDepartmentDto)
        {
            if (!await departmentSpecifies.IsDepartmentNameUniqueAsync(createDepartmentDto.Name))
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Department name must be unique."
                };
            }
            var department = mapper.Map<Department>(createDepartmentDto);
            var userRole = await role.GetRoleByUserId(createDepartmentDto.HeadOfDepartmentId);
            if (userRole != "Teacher")
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Head of Department must have the role of Teacher."
                };
            }
            var AddedDepartmentId = await departmentInterface.AddAsync(department);
            return new ServiceResponse
            {
                Success = true,
                Message = $"Department created successfully with ID: {AddedDepartmentId}"
            };
        }

        public async Task<ServiceResponse> DeleteDepartmentAsync(Guid id)
        {
            var result = await departmentInterface.DeleteAsync(id);
            return result > 0
                ? new ServiceResponse { Success = true, Message = "Department deleted successfully." }
                : new ServiceResponse { Success = false, Message = "Department deletion failed." };
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            var departments = await departmentInterface.GetAllAsync();
            var departmentDtos = mapper.Map<IEnumerable<DepartmentDto>>(departments);
            return departmentDtos;
        }

        public async Task<DepartmentDto> GetDepartmentByIdAsync(Guid id)
        {
            var department = await departmentInterface.GetByIdAsync(id);
            var departmentDto = mapper.Map<DepartmentDto>(department);
            return departmentDto;
        }

        public async Task<ServiceResponse> UpdateDepartmentAsync(Guid id, UpdateDepartmentDto updateDepartmentDto)
        {
            var existingDepartment = await departmentInterface.GetByIdAsync(id);
            if (existingDepartment == null)
            {
                return new ServiceResponse { Success = false, Message = "NotFound" };
            }
            mapper.Map(updateDepartmentDto, existingDepartment);
            var result = await departmentInterface.UpdateAsync(existingDepartment);
            return result > 0 ? new ServiceResponse { Success = true, Message = "Department Updated successfully." } : new ServiceResponse { Success = false, Message = "Failled " };
        }
    }
}
