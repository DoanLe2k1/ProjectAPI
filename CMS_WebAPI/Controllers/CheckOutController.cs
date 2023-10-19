using CMS_WebAPI.Models;
using CMS_WebAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckOutController : Controller
    {
        private readonly ICheckOutService _checkOutService;
        public CheckOutController(ICheckOutService checkOutService)
        {
            _checkOutService = checkOutService;
        }
        [HttpGet("List Check Outs")]
        public async Task<ActionResult<List<CheckOut>>> GetAllCheckOuts()
        {
            var checkouts = await _checkOutService.GetAllCheckOuts();
            return Ok(checkouts);
        }
        [HttpPost("Add Check Out"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<CheckOut>> AddCourse(CheckOut checkOut)
        {
            var add = await _checkOutService.AddCheckOut(checkOut);
            if (add != null)
            {
                return Ok(new { message = "Thêm Thủ tục thanh toán thành công", checkout = add });
            }
            else
            {
                return BadRequest(new { message = "Thêm Thủ tục thanh toán thất bại" });
            }
        }

        [HttpDelete("Delete Check Out"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteScoreType(int checkOutId)
        {
            var deleted = await _checkOutService.DeleteCheckOut(checkOutId);
            if (deleted)
            {
                return Ok(new { message = "Xóa Thủ tục thanh toán thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy Thủ tục thanh toán" });
            }
        }

        [HttpPut("Update Check Out"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateCourse(int checkOutId, CheckOut checkOut)
        {
            if (checkOutId != checkOut.CheckOutId)
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });

            var updated = await _checkOutService.UpdateScoreType(checkOut);
            if (updated)
            {
                return Ok(new { message = "Cập nhật Thủ tục thanh toán thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy Thủ tục thanh toán" });
            }
        }
    }
}
