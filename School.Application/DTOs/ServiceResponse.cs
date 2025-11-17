namespace School.Application.DTOs
{
    public record ServiceResponse(bool Success = false, string Message = null!);
    public record ServiceResponse<T>(T Data = default!, bool Success = false, string Message = null!);
}
