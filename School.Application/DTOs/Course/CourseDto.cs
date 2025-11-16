namespace School.Application.DTOs.Course
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Code { get; set; } = default!;
        public int Credits { get; set; }
        public Guid DepartmentId { get; set; }
    }

}
