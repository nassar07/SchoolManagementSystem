using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Application.DTOs.Course;
using School.Application.DTOs.Department;
using School.Application.Services.Interfaces;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class adminController(IDepartmentService departmentService , ICourseService courseService) : ControllerBase
    {
        [HttpPost("departments/create")]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentDto createDepartmentDto)
        {
            var result = await departmentService.CreateDepartmentAsync(createDepartmentDto);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("departments/GetById/{id}")]
        public async Task<IActionResult> GetDepartmentById(Guid id)
        {
            var departmentDto = await departmentService.GetDepartmentByIdAsync(id);
            return departmentDto != null ? Ok(departmentDto) : NotFound();
        }

        [HttpGet("departments/GetAll")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departmentDtos = await departmentService.GetAllDepartmentsAsync();
            return Ok(departmentDtos);
        }

        [HttpPut("departments/Update/{id}")]
        public async Task<IActionResult> UpdateDepartment(Guid id,[FromBody] UpdateDepartmentDto updateDepartmentDto)
        {
            var result = await departmentService.UpdateDepartmentAsync(id, updateDepartmentDto);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("departments/delete/{id}")]
        public async Task<IActionResult> DeleteDepartment(Guid id)
        {
            var result = await departmentService.DeleteDepartmentAsync(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }


        [HttpPost("courses/create")]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDto createCourseDto)
        {
            var result = await courseService.CreateCourseAsync(createCourseDto);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("courses/GetById/{id}")]
        public async Task<IActionResult> GetCourseById(Guid id)
        {
            var courseDto = await courseService.GetCourseByIdAsync(id);
            return courseDto != null ? Ok(courseDto) : NotFound();
        }

        [HttpGet("courses/GetAll")]
        public async Task<IActionResult> GetAllCourses()
        {
            var courseDtos = await courseService.GetAllCoursesAsync();
            return Ok(courseDtos);
        }

        [HttpPut("courses/Update/{id}")]
        public async Task<IActionResult> UpdateCourse(Guid id, [FromBody] UpdateCourseDto updateCourseDto)
        {
            var result = await courseService.UpdateCourseAsync(id,updateCourseDto);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("courses/delete/{id}")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            var result = await courseService.DeleteCourseAsync(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }

    }
}
