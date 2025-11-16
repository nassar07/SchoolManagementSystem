using School.Application.DTOs;
using School.Application.DTOs.Class;
using School.Application.Services.Interfaces;

namespace School.Application.Services.Implementation
{
    public class ClassService : IClassService
    {
        public Task<ServiceResponse> CreateClassAsync(CreateClassDto createClasstDto)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> DeleteClassAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ClassDto>> GetAllClassesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ClassDto> GetClassByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> UpdateClassAsync(Guid id, UpdateClassDto updateClassDto)
        {
            throw new NotImplementedException();
        }
    }
}
