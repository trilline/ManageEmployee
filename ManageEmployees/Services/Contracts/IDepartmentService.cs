using ManageEmployees.Dtos.Department;
using ManageEmployees.Entities;

namespace ManageEmployees.Services.Contracts
{
    public interface IDepartmentService
    {
        Task<ReadDepartment> CreateDepartmentAsync(CreateDepartment department);

        Task<List<ReadDepartment>> GetDepartments();

        Task<Department> GetDepartmentByIdAsync(int departmentId);
        Task UpdateDepartmentAsync(int departmentId, UpdateDepartment department);

        Task DeleteDepartmentByIdAsync(int departmentId);
        Task<ReadDepartment> GetDepartmentByNameAsync(string name);
    }
}
