using School.Domain.Entities.Identity;

namespace School.Domain.Entities
{
    public class Class : BaseEntity
    {
        public string Name { get; set; } = default!;
        public int Semester { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;

        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public Guid TeacherId { get; set; }
        public ApplicationUser Teacher { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }

        public ICollection<StudentClass> Students { get; set; } = new List<StudentClass>();
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
    }





}
