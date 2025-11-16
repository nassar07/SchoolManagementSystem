namespace School.Application.DTOs.Class
{
    public class CreateClassDto
    {
        public string Name { get; set; } = default!;
        public int Semester { get; set; }
        public Guid CourseId { get; set; }
        public Guid TeacherId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

}
