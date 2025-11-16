using School.Domain.Entities.Identity;

namespace School.Domain.Entities
{
    public class Assignment : BaseEntity
    {
        public Guid ClassId { get; set; }
        public Class Class { get; set; }

        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public Guid CreatedByTeacherId { get; set; }
        public ApplicationUser CreatedByTeacher { get; set; }

        public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
    }





}
