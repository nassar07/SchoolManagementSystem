using School.Application.DTOs;
using School.Application.DTOs.Department;

namespace School.Application.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();
        Task<DepartmentDto> GetDepartmentByIdAsync(Guid id);
        Task<ServiceResponse> CreateDepartmentAsync(CreateDepartmentDto createDepartmentDto);
        Task<ServiceResponse> UpdateDepartmentAsync(Guid id, UpdateDepartmentDto updateDepartmentDto);
        Task<ServiceResponse> DeleteDepartmentAsync(Guid id);
    }
}
