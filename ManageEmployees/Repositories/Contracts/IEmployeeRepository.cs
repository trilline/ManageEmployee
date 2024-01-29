using ManageEmployees.Entities;

namespace ManageEmployees.Repositories.Contracts
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Gets the employees asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<List<Employee>> GetEmployeesAsync();

        /// <summary>
        /// Creates the employee asynchronous.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns></returns>
        Task<Employee> CreateEmployeeAsync(Employee employee);

        /// <summary>
        /// Gets the employee by identifier asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        Task<Employee> GetEmployeeByIdAsync(int employeeId);

        /// <summary>
        /// Updates the employee asynchronous.
        /// </summary>
        /// <param name="employeeToUpdate">The employee to update.</param>
        /// <returns></returns>
        Task UpdateEmployeeAsync(Employee employeeToUpdate);

        /// <summary>
        /// Deletes the employee by identifier asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        Task DeleteEmployeeByIdAsync(int employeeId);

        /// <summary>
        /// Exists the asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        Task<bool> ExistAsync(string email);
    }
}
