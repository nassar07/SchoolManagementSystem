using School.Domain.Entities;

namespace School.Domain.Interfaces.StudentClassSpecifies
{
    public interface IStudentClass
    {
        Task<IEnumerable<StudentClass?>> GetStudentClassByStudentIdAsync(Guid studentId);
    }
}
