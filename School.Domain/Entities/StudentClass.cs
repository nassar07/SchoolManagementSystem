using School.Domain.Entities.Identity;

namespace School.Domain.Entities
{
    public class StudentClass : BaseEntity
    {
        public Guid StudentId { get; set; }
        public ApplicationUser Student { get; set; }

        public Guid ClassId { get; set; }
        public Class Class { get; set; }

        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
    }





}
