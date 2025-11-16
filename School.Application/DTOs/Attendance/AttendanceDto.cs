namespace School.Application.DTOs.Attendance
{
    public class AttendanceDto
    {
        public Guid Id { get; set; }
        public Guid ClassId { get; set; }
        public Guid StudentId { get; set; }
        public string Status { get; set; } = default!;
        public DateTime Date { get; set; }
        public Guid MarkedByTeacherId { get; set; }
    }

}
