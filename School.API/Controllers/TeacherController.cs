using Microsoft.AspNetCore.Mvc;
using School.Application.DTOs.Attendance;
using School.Application.DTOs.Class;
using School.Application.DTOs.Department;
using School.Application.DTOs.StudentClass;
using School.Application.Services.Implementation;
using School.Application.Services.Interfaces;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class teacherController(IClassService classService, IAttendanceService attendanceService) : ControllerBase
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


    }
}
