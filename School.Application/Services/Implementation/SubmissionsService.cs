using AutoMapper;
using Microsoft.AspNetCore.Http;
using School.Application.DTOs;
using School.Application.DTOs.Submission;
using School.Application.Services.Interfaces;
using School.Domain.Entities;
using School.Domain.Interfaces;
using School.Domain.Interfaces.Authentication;

namespace School.Application.Services.Implementation
{
    public class SubmissionsService(IGeneric<Submission> submissionInterface,IGeneric<Assignment> assignmentInterface, IRoleManagement role, IMapper mapper) : ISubmissionsService
    {
        public async Task<ServiceResponse> CreateSubmissionAsync(SubmitAssignmentDto submissionDto)
        {
            bool isStudent = await role.GetRoleByUserId(submissionDto.StudentId) == "Student";
            if (!isStudent)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Only students can submit assignments."
                };
            }
            var assignment = await assignmentInterface.GetByIdAsync(submissionDto.AssignmentId);
            if (assignment == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Assignment not found."
                };
            }
            string fileUrl = await SaveSubmissionFileAsync(submissionDto.File);
            var MappedSubmission = mapper.Map<Submission>(submissionDto);
            MappedSubmission.FileUrl = fileUrl;
            MappedSubmission.SubmittedDate = DateTime.UtcNow;

            var AddedSubmissionId = await submissionInterface.AddAsync(MappedSubmission);
            if (AddedSubmissionId == Guid.Empty)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Failed to create submission."
                };
            }
            return new ServiceResponse
            {
                Success = true,
                Message = "Submission created successfully."
            };

        }

        public async Task<string> SaveSubmissionFileAsync(IFormFile file)
        {
            var uploadsFolder = Path.Combine("wwwroot", "uploads");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string filePath = Path.Combine(uploadsFolder, uniqueName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/uploads/{uniqueName}";
        }
    }
}
