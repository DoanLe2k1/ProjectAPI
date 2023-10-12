using CMS_WebAPI.Models;
using CMS_WebAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HolidayScheduleController : Controller
    {
        private readonly IHolidayScheduleService _holidayScheduleService;
        public HolidayScheduleController(IHolidayScheduleService holidayScheduleService)
        {
            _holidayScheduleService = holidayScheduleService;
        }
        [HttpGet("List Holiday Schedules")]
        public async Task<ActionResult<List<HolidaySchedule>>> GetAllHolidaySchedules()
        {
            var holidaySchedules = await _holidayScheduleService.GetAllHolidaySchedules();
            return Ok(holidaySchedules);
        }
        [HttpGet("Search Holiday Schedule")]
        public IActionResult SearchDepartments(string keyword)
        {
            var holidaySchedule = _holidayScheduleService.SearchHolidaySchedules(keyword);
            return Ok(holidaySchedule);
        }
        [HttpPost("Add Holiday Schedule"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<HolidaySchedule>> AddHolidaySchedule(HolidaySchedule holidaySchedule)
        {
            var add = await _holidayScheduleService.AddHolidaySchedule(holidaySchedule);
            if (add != null)
            {
                return Ok(new { message = "Thêm Kỳ nghỉ thành công", department = add });
            }
            else
            {
                return BadRequest(new { message = "Thêm Kỳ nghỉ thất bại" });
            }
        }

        [HttpDelete("Delete Holiday Schedule"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteHolidaySchedule(int holidayId)
        {
            var deleted = await _holidayScheduleService.DeleteHolidaySchedule(holidayId);
            if (deleted)
            {
                return Ok(new { message = "Xóa Kỳ nghỉ thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy Kỳ nghỉ" });
            }
        }

        [HttpPut("Update Holiday Schedule"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateHolidaySchedule(int holidayId, HolidaySchedule holidaySchedule)
        {
            if (holidayId != holidaySchedule.HolidayId)
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });

            var updated = await _holidayScheduleService.UpdateHolidaySchedule(holidaySchedule);
            if (updated)
            {
                return Ok(new { message = "Cập nhật Kỳ nghỉ thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy Kỳ nghỉ" });
            }
        }
    }
}
