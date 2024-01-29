using ManageEmployees.Entities;

namespace ManageEmployees.Repositories.Contracts
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetDepartmentsAsync();

        Task<Department> GetDepartmentByIDAsync(int Departmentid);
        Task<Department> GetDepartmentByIdWithIncludeAsync(int departmentId);

        Task<Department> GetDepartmentByNameAsync(string departmentName);

        Task UpdateDepartmentAsync(Department departmentToUpdate);

        Task<Department> CreateDepartmentAsync(Department departmentToCreate);

        Task<Department> DeleteDepartmentByIdAsync(int departmentId);

        Task<bool> ExistAsync(string name);
    }
}
