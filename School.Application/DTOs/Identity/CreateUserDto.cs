namespace School.Application.DTOs.Identity
{
    public class CreateUserDto : BaseModel
    {
        public required string Name { get; set; }
        public required string RoleName { get; set; }
        public required string ConfirmPassword { get; set; }
    }

}
