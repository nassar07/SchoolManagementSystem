namespace School.Application.DTOs.Submission
{
    public class SubmitAssignmentDto
    {
        public Guid AssignmentId { get; set; }
        public Guid StudentId { get; set; }
        public string FileUrl { get; set; } = default!;
    }

}
