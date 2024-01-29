using ManageEmployees.Entities;
using ManageEmployees.Infrastructures.Database;
using ManageEmployees.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployees.Repositories
{
    /// <summary>
    /// Dépot de toutes les méthodes crud de l'entité employee
    /// </summary>
    /// <seealso cref="ManageEmployees.Repositories.Contracts.IEmployeeRepository" />
    public class EmployeeRepository : IEmployeeRepository
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly ManageEmployeeDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="System.ArgumentNullException">context</exception>
        public EmployeeRepository(ManageEmployeeDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Gets the employees asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _context.Employees.Include(x => x.Departments).Include(x => x.Attendances).ToListAsync();
        }

        /// <summary>
        /// Creates the employee asynchronous.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns></returns>
        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        /// <summary>
        /// Gets the employee by identifier asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            return await _context.Employees.FirstOrDefaultAsync(x => x.Employeeid == employeeId);
        }

        /// <summary>
        /// Updates the employee asynchronous.
        /// </summary>
        /// <param name="employeeToUpdate">The employee to update.</param>
        public async Task UpdateEmployeeAsync(Employee employeeToUpdate)
        {
            _context.Employees.Update(employeeToUpdate);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes the employee by identifier asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        public async Task DeleteEmployeeByIdAsync(int employeeId)
        {
            var employeeToDelete = await _context.Employees.FindAsync(employeeId);

            if (employeeToDelete != null)
            {
                _context.Employees.Remove(employeeToDelete);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Exists the asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public async Task<bool> ExistAsync(string email)
        {
            return await _context.Employees.AnyAsync(x => x.Email == email);
        }
    }
}
