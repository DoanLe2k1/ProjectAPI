using CMS_WebAPI.Models;
using CMS_WebAPI.Data;
using CMS_WebAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Education_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RevenueController : Controller
    {
        private readonly IRevenueService _revenueService;
        private readonly CMS_WebAPIDbContext _dbContext;
        public RevenueController(IRevenueService revenueService, CMS_WebAPIDbContext dbContext)
        {
            _revenueService = revenueService;
            _dbContext = dbContext;
        }
        [HttpGet("List Revenues")]
        public async Task<ActionResult<List<Revenue>>> GetAllRevenues()
        {
            var revenues = await _revenueService.GetAllRevenues();
            return Ok(revenues);
        }
        [HttpPost("Add Revenue"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<Revenue>> AddRevenue(Revenue revenue)
        {
            var add = await _revenueService.AddRevenue(revenue);
            if (add != null)
            {
                return Ok(new { message = "Thêm Doanh thu môn thành công", revenue = add });
            }
            else
            {
                return BadRequest(new { message = "Thêm Doanh thu thất bại" });
            }
        }

        [HttpDelete("Delete Revenue"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteRevenue(int revenueId)
        {
            var deleted = await _revenueService.DeleteRevenue(revenueId);
            if (deleted)
            {
                return Ok(new { message = "Xóa Doanh thu thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy Doanh thu" });
            }
        }

        [HttpPut("Update Revenue"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateRevenue(int revenueId, Revenue revenue)
        {
            if (revenueId != revenue.RevenueId)
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });

            var updated = await _revenueService.UpdateRevenue(revenue);
            if (updated)
            {
                return Ok(new { message = "Cập nhật Doanh thu thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy Doanh thu" });
            }
        }
        [HttpGet("Get PDF File")]
        public IActionResult GeneratePdf()
        {
            List<Revenue> revenueData = _dbContext.Revenues.ToList(); // Lấy dữ liệu từ bảng Revenue

            byte[] pdfBytes = _revenueService.GeneratePdf(revenueData);

            // Xuất file PDF
            return File(pdfBytes, "application/pdf", "DoanhThu.pdf");
        }
    }
}
