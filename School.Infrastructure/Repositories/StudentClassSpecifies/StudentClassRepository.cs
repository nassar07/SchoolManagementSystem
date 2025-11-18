using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;
using School.Domain.Interfaces.StudentClassSpecifies;
using School.Infrastructure.Data;

namespace School.Infrastructure.Repositories.StudentClassSpecifies
{
    public class StudentClassRepository(AppDbContext context) : IStudentClass
    {
        public async Task<IEnumerable<StudentClass?>> GetStudentClassByStudentIdAsync(Guid studentId)
        {
            return await context.StudentClasses
                .Where(sc => sc.StudentId == studentId)
                .ToListAsync();
        }
    }
}
