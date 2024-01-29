using ManageEmployees.Dtos.Employee;

namespace ManageEmployees.Services.Contracts
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Gets the employees asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<List<ReadEmployee>> GetEmployeesAsync();

        /// <summary>
        /// Gets the employee by identifier asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        Task<ReadEmployee> GetEmployeeByIdAsync(int employeeId);

        /// <summary>
        /// Creates the employee asynchronous.
        /// </summary>
        /// <param name="createEmployee">The create employee.</param>
        /// <returns></returns>
        Task<ReadEmployee> CreateEmployeeAsync(CreateEmployee createEmployee);

        /// <summary>
        /// Updates the employee asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="updateEmployee">The update employee.</param>
        /// <returns></returns>
        Task UpdateEmployeeAsync(int employeeId, UpdateEmployee updateEmployee);

        /// <summary>
        /// Deletes the employee by identifier asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        Task DeleteEmployeeByIdAsync(int employeeId);

        Task<ReadEmployee> CreateEmployeeAsync(CreateEmployeeWithDepartmentID createEmployeeWithDepartmentID);
    }
}
