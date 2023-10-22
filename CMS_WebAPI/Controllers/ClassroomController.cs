using CMS_WebAPI.Models;
using CMS_WebAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassroomController : Controller
    {
        private readonly IClassroomService _classroomService;
        public static IWebHostEnvironment _environment;
        public ClassroomController(IClassroomService classroomService, IWebHostEnvironment webHostEnvironment)
        {
            _classroomService = classroomService;
            _environment = webHostEnvironment;
        }
        [HttpGet("List Classrooms")]
        public async Task<ActionResult<List<Classroom>>> GetAllClassrooms()
        {
            var classrooms = await _classroomService.GetAllClassrooms();
            return Ok(classrooms);
        }
        [HttpGet]
        [Route("Get Classroom Info")]
        public IEnumerable<ClassroomInfo> GetClassroom()
        {
            return _classroomService.GetClassroom().ToList();
        }
        [HttpGet("Search Classroom")]
        public IActionResult SearchClassrooms(string keyword)
        {
            var classrooms = _classroomService.SearchClassrooms(keyword);
            return Ok(classrooms);
        }
        [HttpPost("Add Classroom")]
        public async Task<ActionResult<Classroom>> AddCourse(Classroom classroom)
        {
            var add = await _classroomService.AddClassroom(classroom);
            if (add != null)
            {
                return Ok(new { message = "Thêm Lớp học thành công", classroom = add });
            }
            else
            {
                return BadRequest(new { message = "Thêm Lớp học thất bại" });
            }
        }

        [HttpDelete("Delete Classroom"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteClassroom(int classroomId)
        {
            var deleted = await _classroomService.DeleteClassroom(classroomId);
            if (deleted)
            {
                return Ok(new { message = "Xóa Lớp học thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy Lớp học" });
            }
        }

        [HttpPut("Update Classroom"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateClassroom(int classroomId, Classroom classroom)
        {
            if (classroomId != classroom.ClassroomId)
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });

            var updated = await _classroomService.UpdateClassroom(classroom);
            if (updated)
            {
                return Ok(new { message = "Cập nhật Lớp học thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy Lớp học" });
            }
        }
        [HttpPost("Add-Update Avatar"), Authorize(Roles = "Admin")]
        public IActionResult AddOrUpdateAvatar(int classroomId, IFormFile file)
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
            string uploadsFolder = Path.Combine(_environment.WebRootPath, "Classroom", "Avatars");

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
            _classroomService.AddOrUpdateAvatar(classroomId, filePath);

            return Ok();
        }
        [HttpDelete("Delete Avatar"), Authorize(Roles = "Admin")]
        public IActionResult DeleteAvatar(int classroomId)
        {
            // Gọi phương thức RemoveAvatar trong repository
            _classroomService.DeleteAvatar(classroomId);

            return Ok();
        }
    }
}
