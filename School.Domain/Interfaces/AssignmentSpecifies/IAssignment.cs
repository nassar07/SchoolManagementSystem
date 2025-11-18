using School.Domain.Entities;

namespace School.Domain.Interfaces.AssignmentSpecifies
{
    public interface IAssignment
    {
        Task<Assignment> GetAssignmentByClassIdAsync(Guid classId);
    }
}
