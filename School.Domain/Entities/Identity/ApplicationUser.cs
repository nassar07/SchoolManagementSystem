using Microsoft.AspNetCore.Identity;

namespace School.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Name { get; set; } = default!;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }

        public ICollection<Class> TeachingClasses { get; set; } = new List<Class>();

        public ICollection<StudentClass> Enrollments { get; set; } = new List<StudentClass>();

        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>(); 
        public ICollection<Attendance> MarkedAttendances { get; set; } = new List<Attendance>(); 

        public ICollection<Assignment> CreatedAssignments { get; set; } = new List<Assignment>();
        public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
        public ICollection<Submission> GradedSubmissions { get; set; } = new List<Submission>();
    }

}
