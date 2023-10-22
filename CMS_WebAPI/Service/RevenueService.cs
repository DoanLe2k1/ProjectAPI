using iTextSharp.text.pdf;
using iTextSharp.text;
using CMS_WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using CMS_WebAPI.Data;

namespace CMS_WebAPI.Service
{
    public class RevenueService : IRevenueService
    {
        private readonly CMS_WebAPIDbContext _dbContext;
        public async Task<List<Revenue>> GetAllRevenues()
        {
            return await _dbContext.Revenues.ToListAsync();
        }
        public RevenueService(CMS_WebAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Revenue> AddRevenue(Revenue revenue)
        {
            _dbContext.Revenues.Add(revenue);
            await _dbContext.SaveChangesAsync();
            return revenue;
        }

        public async Task<bool> DeleteRevenue(int revenueId)
        {
            var revenue = await _dbContext.Revenues.FindAsync(revenueId);
            if (revenue == null)
                return false;
            _dbContext.Revenues.Remove(revenue);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateRevenue(Revenue revenue)
        {
            _dbContext.Entry(revenue).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public byte[] GeneratePdf(List<Revenue> revenueData)
        {
            using (MemoryStream outputStream = new MemoryStream())
            {
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, outputStream);

                document.Open();
                // Tạo bảng trong tài liệu PDF
                PdfPTable table = new PdfPTable(5); // Số cột trong bảng

                // Thêm tiêu đề cột
                table.AddCell("RevenueId");
                table.AddCell("StudentId");
                table.AddCell("ClassroomId");
                table.AddCell("Price");
                table.AddCell("TeacherId");

                // Thêm dữ liệu từ bảng Revenue vào bảng PDF
                foreach (var revenue in revenueData)
                {
                    table.AddCell(revenue.RevenueId.ToString());
                    table.AddCell(revenue.StudentId.ToString());
                    table.AddCell(revenue.ClassroomId.ToString());
                    table.AddCell(revenue.Price.ToString());
                    table.AddCell(revenue.TeacherId.ToString());
                }

                // Thêm bảng vào tài liệu PDF
                document.Add(table);

                document.Close();

                return outputStream.ToArray();
            }
        }
    }
}
