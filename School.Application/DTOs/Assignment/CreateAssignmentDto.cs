namespace School.Application.DTOs.Assignment
{
    public class CreateAssignmentDto
    {
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public Guid ClassId { get; set; }
        public Guid CreatedByTeacherId { get; set; }
    }

}
