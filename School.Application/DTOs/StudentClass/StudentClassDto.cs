namespace School.Application.DTOs.StudentClass
{
    public class StudentClassDto
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid ClassId { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }

}
