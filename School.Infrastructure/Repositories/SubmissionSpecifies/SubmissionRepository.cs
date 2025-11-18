using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;
using School.Domain.Interfaces.SubmissionSpecifies;
using School.Infrastructure.Data;

namespace School.Infrastructure.Repositories.SubmissionSpecifies
{
    public class SubmissionRepository(AppDbContext context) : ISubmission
    {
        public async Task<Submission> GetSubmissionByStudentIdAndAssignmentId(Guid studentId, Guid assignmentId)
        {
            var submission = await context.Submissions
                .FirstOrDefaultAsync(s => s.StudentId == studentId && s.AssignmentId == assignmentId);
            return submission!;
        }
    }
}
