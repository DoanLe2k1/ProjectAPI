using CMS_WebAPI.Models;
using CMS_WebAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TranscriptController : ControllerBase
    {
        private readonly ITranscriptService _transcriptService;
        public TranscriptController(ITranscriptService transcriptService)
        {
            _transcriptService = transcriptService;
        }
        
        [HttpGet("List Transcripts")]
        public async Task<ActionResult<List<Transcript>>> GetAllTranscripts()
        {
            var transcripts = await _transcriptService.GetAllTranscripts();
            return Ok(transcripts);
        }
        [HttpGet]
        [Route("Get Score")]
        public IEnumerable<StudentScore> GetScore()
        {
            return _transcriptService.GetScore().ToList();
        }

        [HttpPost("Add Transcript"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<Transcript>> AddTranscripts(Transcript transcript)
        {
            var add = await _transcriptService.AddTranscript(transcript);
            if (add != null)
            {
                return Ok(new { message = "Thêm bảng điểm thành công", transcript = add });
            }
            else
            {
                return BadRequest(new { message = "Thêm bảng điểm thất bại" });
            }
        }

        [HttpDelete("Delete Transcript"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteTranscript(int transcriptId)
        {
            var deleted = await _transcriptService.DeleteTranscript(transcriptId);
            if (deleted)
            {
                return Ok(new { message = "Xóa bảng điểm thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy sinh viên" });
            }
        }

        [HttpPut("Update Transcript"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateTeacher(int transcriptId, Transcript transcript)
        {
            if (transcriptId != transcript.TranscriptId)
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });

            var updated = await _transcriptService.UpdateTranscript(transcript);
            if (updated)
            {
                return Ok(new { message = "Cập nhật bảng điểm thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy bảng điểm" });
            }
        }
        [HttpGet("Search Transcript")]
        public IActionResult SearchTeachers(string keyword)
        {
            var transcripts = _transcriptService.SearchTranscript(keyword);
            return Ok(transcripts);
        }
    }
}
