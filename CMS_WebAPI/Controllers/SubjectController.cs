using CMS_WebAPI.Models;
using CMS_WebAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }
        [HttpGet("List Subjects")]
        public async Task<ActionResult<List<Subject>>> GetAllSubjects()
        {
            var subjects = await _subjectService.GetAllSubjects();
            return Ok(subjects);
        }
        [HttpGet("Search Subject")]
        public IActionResult SearchSubjects(string keyword)
        {
            var subjects = _subjectService.SearchSubjects(keyword);
            return Ok(subjects);
        }
        [HttpPost("Add Subject"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<Subject>> AddSubject(Subject subject)
        {
            var add = await _subjectService.AddSubject(subject);
            if (add != null)
            {
                return Ok(new { message = "Thêm môn thành công", subject = add });
            }
            else
            {
                return BadRequest(new { message = "Thêm môn thất bại" });
            }
        }

        [HttpDelete("Delete Subject"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteSubject(int subjectId)
        {
            var deleted = await _subjectService.DeleteSubject(subjectId);
            if (deleted)
            {
                return Ok(new { message = "Xóa môn thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy môn" });
            }
        }

        [HttpPut("Update Subject"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateSubject(int subjectId, Subject subject)
        {
            if (subjectId != subject.SubjectId)
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });

            var updated = await _subjectService.UpdateSubject(subject);
            if (updated)
            {
                return Ok(new { message = "Cập nhật môn thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy môn" });
            }
        }
        
    }
}
