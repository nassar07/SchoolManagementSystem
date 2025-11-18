using AutoMapper;
using School.Application.DTOs;
using School.Application.DTOs.Assignment;
using School.Application.DTOs.Submission;
using School.Application.Services.Interfaces;
using School.Domain.Entities;
using School.Domain.Interfaces;
using School.Domain.Interfaces.AssignmentSpecifies;
using School.Domain.Interfaces.Authentication;
using School.Domain.Interfaces.SubmissionSpecifies;

namespace School.Application.Services.Implementation
{
    public class AssignmentService(IGeneric<Assignment> assignmentInterface, 
        IMapper mapper, 
        IGeneric<Submission> submissionInterface, 
        IGeneric<Class> classInterface,
        IAssignment assignmentRepository,
        ISubmission submissionRepository,
        IRoleManagement role) : IAssignmentService
    {
        public async Task<ServiceResponse> CreateAssignmentAsync(CreateAssignmentDto createAssignmentDto)
        {
            var IsClassExixt = await classInterface.GetByIdAsync(createAssignmentDto.ClassId);
            if (IsClassExixt == null)
            {
                return new ServiceResponse
                {
                    Message = "Class does not exist.",
                    Success = false
                };
            }
            var IsTeacherRole = await role.GetRoleByUserId(createAssignmentDto.CreatedByTeacherId);
            if (IsTeacherRole != "Teacher")
            {
                return new ServiceResponse
                {
                    Message = "Only teachers can create assignments.",
                    Success = false
                };
            }
            var MappedAssignment = mapper.Map<Assignment>(createAssignmentDto);
            var AddedAssignmentId = await assignmentInterface.AddAsync(MappedAssignment);
            return new ServiceResponse
            {
                Message = $"Assignment Created With Id : {AddedAssignmentId}",
                Success = true
            };
        }

        public async Task<IEnumerable<AssignmentDto>> GetAllAssignmentsAsync()
        {
            var Assignments = await assignmentInterface.GetAllAsync();
            var result = mapper.Map<IEnumerable<AssignmentDto>>(Assignments);
            return result;
        }

        public async Task<AssignmentDto> GetAssignmentByClassIdAsync(Guid classId)
        {
            var assignment = await assignmentRepository.GetAssignmentByClassIdAsync(classId);
            var mappedAssignment = mapper.Map<AssignmentDto>(assignment);
            return mappedAssignment;

        }

        public async Task<ServiceResponse<IEnumerable<StudentGradeDto>>> GetStudentGradesAsync(Guid studentId)
        {
            var isStudentExist = await role.GetRoleByUserId(studentId);
            if(isStudentExist != "Student")
            {
                return new ServiceResponse<IEnumerable<StudentGradeDto>>
                {
                    Message = "User is not a student.",
                    Success = false,
                    Data = null
                };
            }
            var grades = await submissionRepository.GetStudentGradesByStudentId(studentId);
            var mappedGrades = mapper.Map<IEnumerable<StudentGradeDto>>(grades);
            return new ServiceResponse<IEnumerable<StudentGradeDto>>
            {
                Message = "Student grades retrieved successfully.",
                Success = true,
                Data = mappedGrades
            };
        }

        public async Task<ServiceResponse> GradeStudentSubmissions(Guid assignmentId, Guid StudentId, GradeSubmissionDto gradeSubmissionDto)
        {
            var submission = await submissionRepository.GetSubmissionByStudentIdAndAssignmentId(StudentId, assignmentId); 
            if (submission == null)
            {
                return new ServiceResponse
                {
                    Message = "Submission not found.",
                    Success = false
                };
            }
            submission.Grade = gradeSubmissionDto.Grade;
            var IsTeacherRole = await role.GetRoleByUserId(gradeSubmissionDto.GradedByTeacherId);
            if (IsTeacherRole != "Teacher")
            {
                return new ServiceResponse
                {
                    Message = "Only teachers can grade submissions.",
                    Success = false
                };
            }
            submission.GradedByTeacherId = gradeSubmissionDto.GradedByTeacherId;
            submission.Remarks = gradeSubmissionDto.Remarks;
            await submissionInterface.UpdateAsync(submission);
            return new ServiceResponse
            {
                Message = "Submission graded successfully.",
                Success = true
            };
        }

        
    }
}
