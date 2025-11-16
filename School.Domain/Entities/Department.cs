using School.Domain.Entities.Identity;

namespace School.Domain.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        public Guid HeadOfDepartmentId { get; set; }
        public ApplicationUser HeadOfDepartment { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }


}
