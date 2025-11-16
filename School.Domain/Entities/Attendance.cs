using School.Domain.Entities.Identity;
using School.Domain.Enums;

namespace School.Domain.Entities
{
    public class Attendance : BaseEntity
    {
        public Guid ClassId { get; set; }
        public Class Class { get; set; }

        public Guid StudentId { get; set; }
        public ApplicationUser Student { get; set; }

        public DateTime Date { get; set; }
        public AttendanceStatus Status { get; set; }

        public Guid MarkedByTeacherId { get; set; }
        public ApplicationUser MarkedByTeacher { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
