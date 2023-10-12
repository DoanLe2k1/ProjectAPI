using CMS_WebAPI.Models;
using CMS_WebAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace CMS_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public static IWebHostEnvironment _environment;
        public StudentController(IStudentService studentService, IWebHostEnvironment webHostEnvironment)
        {
            _studentService = studentService;
            _environment = webHostEnvironment;
        }

        [HttpGet("List Students")]
        public async Task<ActionResult<List<Student>>> GetAllStudents()
        {
            var students = await _studentService.GetAllStudents();
            return Ok(students);
        }
        [HttpGet("Search Student")]
        public IActionResult SearchStudents(string keyword)
        {
            var students = _studentService.SearchStudents(keyword);
            return Ok(students);
        }
        [HttpPost("Add Student"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<Student>> AddStudent(Student student)
        {
            var addedStudent = await _studentService.AddStudent(student);
            if (addedStudent != null)
            {
                return Ok(new { message = "Thêm sinh viên thành công", student = addedStudent });
            }
            else
            {
                return BadRequest(new { message = "Thêm sinh viên thất bại" });
            }
        }

        [HttpDelete("Delete Student"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteStudent(int studentId)
        {
            var deleted = await _studentService.DeleteStudent(studentId);
            if (deleted)
            {
                return Ok(new { message = "Xóa sinh viên thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy sinh viên" });
            }
        }

        [HttpPut("Update Student"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateStudent(int studentId, Student student)
        {
            if (studentId != student.StudentId)
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });

            var updated = await _studentService.UpdateStudent(student);
            if (updated)
            {
                return Ok(new { message = "Cập nhật sinh viên thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy sinh viên" });
            }
        }
        [HttpPost("Add-Update Avatar"), Authorize(Roles = "Admin")]
        public IActionResult AddOrUpdateAvatar(int studentId, IFormFile file)
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
            string uploadsFolder = Path.Combine(_environment.WebRootPath, "Student","Avatars");

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
            _studentService.AddOrUpdateAvatar(studentId, filePath);

            return Ok();
        }
        [HttpDelete("Delete Avatar"), Authorize(Roles = "Admin")]
        public IActionResult DeleteeAvatar(int studentId)
        {
            // Gọi phương thức RemoveAvatar trong repository
            _studentService.DeleteAvatar(studentId);

            return Ok();
        }
    }
}
