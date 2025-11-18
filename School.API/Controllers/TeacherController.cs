using Microsoft.AspNetCore.Mvc;
using School.Application.DTOs.Assignment;
using School.Application.DTOs.Attendance;
using School.Application.DTOs.Class;
using School.Application.DTOs.StudentClass;
using School.Application.DTOs.Submission;
using School.Application.Services.Interfaces;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class teacherController(IClassService classService, IAttendanceService attendanceService, IAssignmentService assignmentService) : ControllerBase
    {
        [HttpPost("classes/Create")]
        public async Task<IActionResult> CreateClass([FromBody] CreateClassDto createClassDto)
        {
            var result = await classService.CreateClassAsync(createClassDto);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("classes/GetById/{id}")]
        public async Task<IActionResult> GetClassById(Guid id)
        {
            var classDto = await classService.GetClassByIdAsync(id);
            return classDto != null ? Ok(classDto) : NotFound();
        }

        [HttpGet("classes/GetAll")]
        public async Task<IActionResult> GetAllClasses()
        {
            var classDtos = await classService.GetAllClassesAsync();
            return Ok(classDtos);
        }

        [HttpPut("classes/Update/{id}")]
        public async Task<IActionResult> UpdateClass(Guid id, [FromBody] UpdateClassDto updateClassDto)
        {
            var result = await classService.UpdateClassAsync(id, updateClassDto);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPut("classes/Deactivate/{id}")]
        public async Task<IActionResult> DeactivateClass(Guid id)
        {
            var result = await classService.DeactivateClassAsync(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("classes/delete/{id}")]
        public async Task<IActionResult> DeleteClass(Guid id)
        {
            var result = await classService.DeleteClassAsync(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("classes/AssignStudentToClass")]
        public async Task<IActionResult> AssignStudentToClass(EnrollStudentDto enrollStudentDto)
        {
            var result = await classService.AssignStudentToClassAsync(enrollStudentDto);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("attendance")]
        public async Task<IActionResult> MarkAttendance([FromBody] MarkAttendanceDto markAttendanceDto)
        {
            var result = await attendanceService.MarkAttendanceAsync(markAttendanceDto);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpGet("attendance/{classId}")]
        public async Task<IActionResult> GetAttendanceByClassId(Guid classId)
        {
            var result = await attendanceService.GetAttendanceByClassIdAsync(classId);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("assignments")]
        public async Task<IActionResult> CreateAssignment([FromBody] CreateAssignmentDto createAssignmentDto)
        {
            var result = await assignmentService.CreateAssignmentAsync(createAssignmentDto);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpGet("assignments/{classId}")]
        public async Task<IActionResult> GetAssignmentsByClassId(Guid classId)
        {
            var assignmentDto = await assignmentService.GetAssignmentByClassIdAsync(classId);
            return assignmentDto != null ? Ok(assignmentDto) : NotFound();
        }
        [HttpPost("assignments/{assignmentId}/{StudentId}/grade")]
        public async Task<IActionResult> GradeStudentSubmission(Guid assignmentId, Guid StudentId, [FromBody] GradeSubmissionDto gradeSubmissionDto)
        {
            var result = await assignmentService.GradeStudentSubmissions(assignmentId, StudentId, gradeSubmissionDto);
            return result.Success ? Ok(result) : BadRequest(result);
        }


    }
}
