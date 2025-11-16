namespace School.Domain.Entities
{
    public class Course : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Code { get; set; } = default!;
        public string? Description { get; set; }
        public int Credits { get; set; }

        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }

        public ICollection<Class> Classes { get; set; } = new List<Class>();
    }





}
