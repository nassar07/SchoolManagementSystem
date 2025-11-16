namespace School.Application.DTOs.Assignment
{
    public class UpdateAssignmentDto
    {
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
    }

}
