using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;
using School.Domain.Interfaces.AssignmentSpecifies;
using School.Infrastructure.Data;

namespace School.Infrastructure.Repositories.AssignmentSpecifies
{
    public class AssignmentRepository(AppDbContext context) : IAssignment
    {
        public async Task<Assignment> GetAssignmentByClassIdAsync(Guid classId)
        {
            var assignment = await context.Assignments
                .FirstOrDefaultAsync(a => a.ClassId == classId);
            return assignment!;
        }
    }
}
