using CMS_WebAPI.Models;
using CMS_WebAPI.Service;
using CMS_WebAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [HttpGet("List Courses")]
        public async Task<ActionResult<List<Course>>> GetAllCourses()
        {
            var courses = await _courseService.GetAllCourses();
            return Ok(courses);
        }
        [HttpPost("Add Course"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<Course>> AddCourse(Course course)
        {
            var add = await _courseService.AddCourse(course);
            if (add != null)
            {
                return Ok(new { message = "Thêm Khóa thành công", course = add });
            }
            else
            {
                return BadRequest(new { message = "Thêm Khóa thất bại" });
            }
        }

        [HttpDelete("Delete Course"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteCourse(int courseId)
        {
            var deleted = await _courseService.DeleteCourse(courseId);
            if (deleted)
            {
                return Ok(new { message = "Xóa Khóa thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy Khóa" });
            }
        }

        [HttpPut("Update Course"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateCourse(int courseId, Course course)
        {
            if (courseId != course.CourseId)
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });

            var updated = await _courseService.UpdateCourse(course);
            if (updated)
            {
                return Ok(new { message = "Cập nhật Khóa thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy Khóa" });
            }
        }
    }  
}
