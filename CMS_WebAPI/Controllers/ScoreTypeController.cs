using CMS_WebAPI.Models;
using CMS_WebAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScoreTypeController : Controller
    {
        private readonly IScoreTypeService _scoreTypeService;
        public ScoreTypeController(IScoreTypeService scoreTypeService)
        {
            _scoreTypeService = scoreTypeService;
        }
        [HttpGet("List Score Types")]
        public async Task<ActionResult<List<ScoreType>>> GetAllScoreTypes()
        {
            var scoreTypes = await _scoreTypeService.GetAllScoreTypes();
            return Ok(scoreTypes);
        }
        [HttpPost("Add Score Type"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ScoreType>> AddCourse(ScoreType scoreType)
        {
            var add = await _scoreTypeService.AddScoreType(scoreType);
            if (add != null)
            {
                return Ok(new { message = "Thêm Loại điểm thành công", scoreType = add });
            }
            else
            {
                return BadRequest(new { message = "Thêm Loại điểm thất bại" });
            }
        }

        [HttpDelete("Delete Score Type"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteScoreType(int scoreTypeId)
        {
            var deleted = await _scoreTypeService.DeleteScoreType(scoreTypeId);
            if (deleted)
            {
                return Ok(new { message = "Xóa Loại điểm thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy Loại điểm" });
            }
        }

        [HttpPut("Update Score Type"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateCourse(int scoreTypeId, ScoreType scoreType)
        {
            if (scoreTypeId != scoreType.ScoreTypeId)
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });

            var updated = await _scoreTypeService.UpdateScoreType(scoreType);
            if (updated)
            {
                return Ok(new { message = "Cập nhật Loại điểm thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy Loại điểm" });
            }
        }
    }
}
