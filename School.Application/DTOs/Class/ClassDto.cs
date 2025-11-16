namespace School.Application.DTOs.Class
{
    public class ClassDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public int Semester { get; set; }
        public Guid CourseId { get; set; }
        public Guid TeacherId { get; set; }
        public bool IsActive { get; set; }
    }

}
