using CMS_WebAPI.Models;

namespace CMS_WebAPI.Service
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetAllDepartments();
        Task<Department> AddDepartment(Department department);
        Task<bool> DeleteDepartment(int departmentId);
        Task<bool> UpdateDepartment(Department department);
        List<Department> SearchDepartments(string keyword);
    }
}
