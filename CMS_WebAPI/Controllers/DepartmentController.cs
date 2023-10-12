using CMS_WebAPI.Models;
using CMS_WebAPI.Service;
using CMS_WebAPI.Data;
using CMS_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CMS_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        [HttpGet("List Departments")]
        public async Task<ActionResult<List<Department>>> GetAllDepartments()
        {
            var departments = await _departmentService.GetAllDepartments();
            return Ok(departments);
        }
        [HttpGet("Search Subject")]
        public IActionResult SearchDepartments(string keyword)
        {
            var departments = _departmentService.SearchDepartments(keyword);
            return Ok(departments);
        }
        [HttpPost("Add Department"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<Department>> AddDepartment(Department department)
        {
            var add = await _departmentService.AddDepartment(department);
            if (add != null)
            {
                return Ok(new { message = "Thêm Tổ - bộ môn thành công", department = add });
            }
            else
            {
                return BadRequest(new { message = "Thêm Tổ - bộ môn thất bại" });
            }
        }

        [HttpDelete("Delete Department"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteDepartment(int departmentId)
        {
            var deleted = await _departmentService.DeleteDepartment(departmentId);
            if (deleted)
            {
                return Ok(new { message = "Xóa Tổ - bộ môn thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy Tổ - bộ môn" });
            }
        }

        [HttpPut("Update Course"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateCourse(int departmentId, Department department)
        {
            if (departmentId != department.DepartmentId)
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });

            var updated = await _departmentService.UpdateDepartment(department);
            if (updated)
            {
                return Ok(new { message = "Cập nhật Tổ - bộ môn thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy Tổ - bộ môn" });
            }
        }
    }
}
