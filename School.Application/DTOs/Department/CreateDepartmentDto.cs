namespace School.Application.DTOs.Department
{
    public class CreateDepartmentDto
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public Guid HeadOfDepartmentId { get; set; }
    }

}
