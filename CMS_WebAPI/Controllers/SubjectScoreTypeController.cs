using CMS_WebAPI.Models;
using CMS_WebAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectScoreTypeController : Controller
    {
        private readonly ISubjectScoreTypeService _subjectScoreTypeService;
        public SubjectScoreTypeController(ISubjectScoreTypeService subjectScoreTypeService)
        {
            _subjectScoreTypeService = subjectScoreTypeService;
        }
        [HttpGet("List Subject Score Types")]
        public async Task<ActionResult<List<SubjectScoreType>>> GetAllSubjectScoreTypes()
        {
            var subjectScoreTypes = await _subjectScoreTypeService.GetAllSubjectScoreTypes();
            return Ok(subjectScoreTypes);
        }
        [HttpPost("Add Subject Score Type"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<SubjectScoreType>> AddSubjectScoreType(SubjectScoreType subjectScoreType)
        {
            var add = await _subjectScoreTypeService.AddSubjectScoreType(subjectScoreType);
            if (add != null)
            {
                return Ok(new { message = "Thêm Loại điểm môn thành công", subjectScoreType = add });
            }
            else
            {
                return BadRequest(new { message = "Thêm Loại điểm môn thất bại" });
            }
        }

        [HttpDelete("Delete Subject Score Type"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteSubjectScoreType(int subjectScoreTypeId)
        {
            var deleted = await _subjectScoreTypeService.DeleteSubjectScoreType(subjectScoreTypeId);
            if (deleted)
            {
                return Ok(new { message = "Xóa Loại điểm môn thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy Loại điểm môn" });
            }
        }

        [HttpPut("Update Subject Score Type"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateCourse(int subjectScoreTypeId, SubjectScoreType subjectScoreType)
        {
            if (subjectScoreTypeId != subjectScoreType.SubjectScoreTypeId)
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });

            var updated = await _subjectScoreTypeService.UpdateSubjectScoreType(subjectScoreType);
            if (updated)
            {
                return Ok(new { message = "Cập nhật Loại điểm môn thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy Loại điểm môn" });
            }
        }
    }
}
