using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Application.DTOs.Department;
using School.Application.Services.Interfaces;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class AdminController(IDepartmentService departmentService) : ControllerBase
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

    }
}
