namespace School.Application.DTOs.Attendance
{
    public class MarkAttendanceDto
    {
        public Guid ClassId { get; set; }
        public Guid StudentId { get; set; }
        public string Status { get; set; } = default!;
    }

}
