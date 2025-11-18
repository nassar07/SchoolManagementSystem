using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;
using School.Domain.Interfaces.SubmissionSpecifies;
using School.Infrastructure.Data;

namespace School.Infrastructure.Repositories.SubmissionSpecifies
{
    public class SubmissionRepository(AppDbContext context) : ISubmission
    {
        public async Task<Guid> CreateSubmissionAsync(Submission submission)
        {
            await context.Submissions.AddAsync(submission);
            await context.SaveChangesAsync();
            return submission.Id;
        }

        public async Task<IEnumerable<Submission>> GetStudentGradesByStudentId(Guid studentId)
        {
            var submissions = await context.Submissions
                .Where(s => s.StudentId == studentId)
                .AsNoTracking()
                .ToListAsync();
            return submissions;

        }

        public async Task<Submission> GetSubmissionByStudentIdAndAssignmentId(Guid studentId, Guid assignmentId)
        {
            var submission = await context.Submissions
                .FirstOrDefaultAsync(s => s.StudentId == studentId && s.AssignmentId == assignmentId);
            return submission!;
        }
    }
}
