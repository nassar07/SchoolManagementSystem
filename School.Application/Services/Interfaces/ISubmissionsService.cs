using Microsoft.AspNetCore.Http;
using School.Application.DTOs;
using School.Application.DTOs.Submission;

namespace School.Application.Services.Interfaces
{
    public interface ISubmissionsService
    {
        Task<ServiceResponse> CreateSubmissionAsync(SubmitAssignmentDto submissionDto);
        Task<string> SaveSubmissionFileAsync(IFormFile file);
    }
}
