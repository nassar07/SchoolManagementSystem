using School.Domain.Entities;

namespace School.Domain.Interfaces.SubmissionSpecifies
{
    public interface ISubmission
    {
        Task<Submission> GetSubmissionByStudentIdAndAssignmentId(Guid studentId, Guid assignmentId);

    }
}
