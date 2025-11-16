using School.Domain.Entities.Identity;

namespace School.Domain.Entities
{
    public class Submission : BaseEntity
    {
        public Guid AssignmentId { get; set; }
        public Assignment Assignment { get; set; }

        public Guid StudentId { get; set; }
        public ApplicationUser Student { get; set; }

        public DateTime SubmittedDate { get; set; }
        public string FileUrl { get; set; } = default!;
        public double? Grade { get; set; }

        public Guid? GradedByTeacherId { get; set; }
        public ApplicationUser? GradedByTeacher { get; set; }

        public string? Remarks { get; set; }
    }





}
