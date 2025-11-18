using Microsoft.AspNetCore.Mvc;
using School.Application.DTOs.Submission;
using School.Application.Services.Interfaces;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class studentController(IStudentClassService studentClassService, IAttendanceService attendanceService, IAssignmentService assignmentService, ISubmissionsService submissionsService) : ControllerBase
    {
        [HttpGet("classes/{studentId}")]
        public async Task<IActionResult> GetStudentClasses(Guid studentId)
        {
            var studentClasses = await studentClassService.GetClassesBySudetId(studentId);
            return Ok(studentClasses);
        }
        [HttpGet("attendance/{studentId}")]
        public async Task<IActionResult> GetStudentAttendance(Guid studentId)
        {
            var attendanceResponse = await attendanceService.GetAttendanceByStudentIdAsync(studentId);
            if (!attendanceResponse.Success)
            {
                return BadRequest(attendanceResponse.Message);
            }
            return Ok(attendanceResponse.Data);
        }
        [HttpGet("grades/{studentId}")]
        public async Task<IActionResult> GetStudentGrades(Guid studentId)
        {
            var grades = await assignmentService.GetStudentGradesAsync(studentId);
            return Ok(grades);
        }
        [HttpPost("assignments/submit")]
        public async Task<IActionResult> SubmitAssignment([FromForm] SubmitAssignmentDto request)
        {
            var response = await submissionsService.CreateSubmissionAsync(request);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }




    }
}
