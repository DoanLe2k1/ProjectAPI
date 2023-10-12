using CMS_WebAPI.Models;
using CMS_WebAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        public static IWebHostEnvironment _webHostEnvironment;
        public TeacherController(ITeacherService teacherService, IWebHostEnvironment webHostEnvironment)
        {
            _teacherService = teacherService;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet("List Teachers")]
        public async Task<ActionResult<List<Teacher>>> GetAllTeacher()
        {
            var teachers = await _teacherService.GetAllTeacher();
            return Ok(teachers);
        }
        [HttpGet("Search Teacher")]
        public IActionResult SearchTeachers(string keyword)
        {
            var teachers = _teacherService.SearchTeachers(keyword);
            return Ok(teachers);
        }
        [HttpPost("Add Teacher"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<Teacher>> AddTeacher(Teacher teacher)
        {
            var addedteacher = await _teacherService.AddTeacher(teacher);
            if (addedteacher != null)
            {
                return Ok(new { message = "Thêm giáo viên thành công", teacher = addedteacher });
            }
            else
            {
                return BadRequest(new { message = "Thêm giáo viên thất bại" });
            }
        }

        [HttpDelete("Delete Teacher"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteTeacher(int teacherId)
        {
            var deleted = await _teacherService.DeleteTeacher(teacherId);
            if (deleted)
            {
                return Ok(new { message = "Xóa giáo viên thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy sinh viên" });
            }
        }

        [HttpPut("Update Teacher"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateTeacher(int teacherId, Teacher teacher)
        {
            if (teacherId != teacher.TeacherId)
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });

            var updated = await _teacherService.UpdateTeacher(teacher);
            if (updated)
            {
                return Ok(new { message = "Cập nhật giáo viên thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy giáo viên" });
            }
        }
        [HttpPost("Add-Update Avatar"), Authorize(Roles = "Admin")]
        public IActionResult AddOrUpdateAvatar(int teacherId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            // Tạo tên file duy nhất
            string uniqueFileName = Path.GetFileNameWithoutExtension(file.FileName)
                + "_" + Guid.NewGuid().ToString().Substring(0, 8)
                + Path.GetExtension(file.FileName);

            // Xác định thư mục lưu trữ Avatar (ví dụ: wwwroot/Avatars)
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Teacher", "Avatars");

            // Tạo thư mục nếu không tồn tại
            Directory.CreateDirectory(uploadsFolder);

            // Đường dẫn đầy đủ của file avatar
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Lưu file avatar vào thư mục đã chỉ định
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            // Gọi phương thức AddOrUpdateAvatar trong repository
            _teacherService.AddOrUpdateAvatar(teacherId, filePath);

            return Ok();
        }
        [HttpDelete("Delete Avatar"), Authorize(Roles = "Admin")]
        public IActionResult DeleteeAvatar(int teacherId)
        {
            // Gọi phương thức RemoveAvatar trong repository
            _teacherService.DeleteAvatar(teacherId);

            return Ok();
        }
    }
}
