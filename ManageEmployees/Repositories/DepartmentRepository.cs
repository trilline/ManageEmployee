using ManageEmployees.Entities;
using ManageEmployees.Infrastructures.Database;
using ManageEmployees.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployees.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ManageEmployeeDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DepartmentRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public DepartmentRepository(ManageEmployeeDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the departments asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Department>> GetDepartmentsAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        /// <summary>
        /// Creates the department asynchronous.
        /// </summary>
        /// <param name="department">The department.</param>
        /// <returns></returns>
        public async Task<Department> CreateDepartmentAsync( Department department )
        {
            await _context.Departments.AddAsync( department );
            await _context.SaveChangesAsync();
            return department;
        }

        /// <summary>
        /// Gets the department by name asynchronous.
        /// </summary>
        /// <param name="DepartmentName">Name of the department.</param>
        /// <returns></returns>
        public async Task<Department> GetDepartmentByNameAsync(string DepartmentName)
        {
            return await _context.Departments.FirstOrDefaultAsync(x => x.Name == DepartmentName);
        }

        /// <summary>
        /// Gets the department by identifier asynchronous.
        /// </summary>
        /// <param name="Departmentid">The departmentid.</param>
        /// <returns></returns>
        public async Task<Department> GetDepartmentByIDAsync(int Departmentid)
        {
            return await _context.Departments.FirstOrDefaultAsync(x => x.Departmentid == Departmentid);
        }

        /// <summary>
        /// Gets the department by identifier with include asynchronous.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        public async Task<Department> GetDepartmentByIdWithIncludeAsync(int departmentId)
        {
            return await _context.Departments
                .Include(x => x.Employees)
                .FirstOrDefaultAsync(x => x.Departmentid == departmentId);
        }

        /// <summary>
        /// Updates the department asynchronous.
        /// </summary>
        /// <param name="departmentToUpdate">The department to update.</param>
        public async Task UpdateDepartmentAsync(Department departmentToUpdate)
        {
            _context.Departments.Update(departmentToUpdate);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes the department by identifier asynchronous.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        public async Task<Department> DeleteDepartmentByIdAsync(int departmentId)
        {
            var departmentToDelete = await _context.Departments.FindAsync(departmentId);
            _context.Departments.Remove(departmentToDelete);
            await _context.SaveChangesAsync();
            return departmentToDelete;
        }

        public async Task<bool> ExistAsync(string name)
        {
            return await _context.Departments.AnyAsync(x => x.Name == name);
        }
    }
}
