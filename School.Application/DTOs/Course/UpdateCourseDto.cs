namespace School.Application.DTOs.Course
{
    public class UpdateCourseDto
    {
        public string Name { get; set; } = default!;
        public string Code { get; set; } = default!;
        public int Credits { get; set; }
        public Guid DepartmentId { get; set; }
    }

}
