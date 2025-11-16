namespace School.Application.DTOs.Department
{
    public class DepartmentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public Guid HeadOfDepartmentId { get; set; }
    }

}
