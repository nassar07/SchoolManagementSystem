namespace School.Application.DTOs.Submission
{
    public class StudentGradeDto
    {
        public Guid AssignmentId { get; set; }
        public double? Grade { get; set; }
        public Guid GradedByTeacherId { get; set; }
        public string? Remarks { get; set; }
    }
}
