using School.Application.DTOs;
using School.Application.DTOs.Assignment;
using School.Application.DTOs.Submission;

namespace School.Application.Services.Interfaces
{
    public interface IAssignmentService
    {
        Task<IEnumerable<AssignmentDto>> GetAllAssignmentsAsync();
        Task<AssignmentDto> GetAssignmentByClassIdAsync(Guid classId);
        Task<ServiceResponse> CreateAssignmentAsync(CreateAssignmentDto createAssignmentDto);
        Task<ServiceResponse> GradeStudentSubmissions(Guid assignmentId, Guid StudentId, GradeSubmissionDto gradeSubmissionDto);
    }
}
