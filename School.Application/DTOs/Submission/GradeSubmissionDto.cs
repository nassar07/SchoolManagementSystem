namespace School.Application.DTOs.Submission
{
    public class GradeSubmissionDto
    {
        public double Grade { get; set; }
        public Guid GradedByTeacherId { get; set; }
        public string? Remarks { get; set; }
    }

}
