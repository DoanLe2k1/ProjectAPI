using CMS_WebAPI.Models;
using CMS_WebAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace CMS_WebAPI.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly CMS_WebAPIDbContext _dbContext;
        public async Task<List<Department>> GetAllDepartments()
        {
            return await _dbContext.Departments.ToListAsync();
        }
        public DepartmentService(CMS_WebAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Department> AddDepartment(Department department)
        {
            _dbContext.Departments.Add(department);
            await _dbContext.SaveChangesAsync();
            return department;
        }

        public async Task<bool> DeleteDepartment(int departmentId)
        {
            var department = await _dbContext.Departments.FindAsync(departmentId);
            if (department == null)
                return false;
            _dbContext.Departments.Remove(department);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateDepartment(Department department)
        {
            _dbContext.Entry(department).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public List<Department> SearchDepartments(string keyword)
        {
            return _dbContext.Departments.Where(s => s.DepartmentName.Contains(keyword)).ToList();
        }
    }
}
