using ManageEmployees.Dtos.Employee;
using ManageEmployees.Entities;
using ManageEmployees.Repositories.Contracts;
using ManageEmployees.Services.Contracts;

namespace ManageEmployees.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly AutoMapper.IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeService"/> class.
        /// </summary>
        /// <param name="employeeRepository">The employee repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <exception cref="System.ArgumentNullException">
        /// employeeRepository
        /// or
        /// mapper
        /// </exception>
        public EmployeeService(IEmployeeRepository employeeRepository, AutoMapper.IMapper mapper, IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _departmentRepository = departmentRepository;
        }

        /// <summary>
        /// Gets the employees asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReadEmployee>> GetEmployeesAsync()
        {
            var employees = await _employeeRepository.GetEmployeesAsync();
            return employees.Select(employee => _mapper.Map<ReadEmployee>(employee)).ToList();
        }

        /// <summary>
        /// Gets the employee by identifier asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Employee ID should be greater than 0. - employeeId</exception>
        /// <exception cref="System.Exception">Employee with ID {employeeId} not found.</exception>
        public async Task<ReadEmployee> GetEmployeeByIdAsync(int employeeId)
        {
            if (employeeId <= 0)
            {
                throw new ArgumentException("Employee ID should be greater than 0.", nameof(employeeId));
            }

            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
            return _mapper.Map<ReadEmployee>(employee) ?? throw new Exception($"Employee with ID {employeeId} not found.");
        }

        /// <summary>
        /// Creates the employee asynchronous.
        /// </summary>
        /// <param name="createEmployee">The create employee.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">createEmployee</exception>
        /// <exception cref="System.ArgumentException">Email cannot be null or empty. - Email</exception>
        /// <exception cref="System.Exception">
        /// Employee with email {createEmployee.Email} already exists.
        /// or
        /// Failed to create employee.
        /// </exception>
        public async Task<ReadEmployee> CreateEmployeeAsync(CreateEmployee createEmployee)
        {
            if (createEmployee == null)
            {
                throw new ArgumentNullException(nameof(createEmployee));
            }

            if (string.IsNullOrWhiteSpace(createEmployee.Email))
            {
                throw new ArgumentException("Email cannot be null or empty.", nameof(createEmployee.Email));
            }
            // Vérifier le format de l'email
            if (!IsValidEmail(createEmployee.Email))
            {
                throw new ArgumentException("Invalid email format.", nameof(createEmployee.Email));
            }

            // Vérifier le format du numéro de téléphone
            if (!IsValidPhoneNumber(createEmployee.Phonenumber))
            {
                throw new ArgumentException("Invalid phone number format.", nameof(createEmployee.Phonenumber));
            }

            if (await _employeeRepository.ExistAsync(createEmployee.Email))
            {
                throw new Exception($"Employee with email {createEmployee.Email} already exists.");
            }

            var employeeToCreate = _mapper.Map<Employee>(createEmployee);
            var createdEmployee = await _employeeRepository.CreateEmployeeAsync(employeeToCreate);

            return _mapper.Map<ReadEmployee>(createdEmployee) ?? throw new Exception("Failed to create employee.");
        }

        public async Task<ReadEmployee> CreateEmployeeAsync(CreateEmployeeWithDepartmentID createEmployeeWithDepartmentID)
        {
            if (createEmployeeWithDepartmentID == null)
            {
                throw new ArgumentNullException(nameof(createEmployeeWithDepartmentID));
            }

            if (string.IsNullOrWhiteSpace(createEmployeeWithDepartmentID.Email))
            {
                throw new ArgumentException("Email cannot be null or empty.", nameof(createEmployeeWithDepartmentID.Email));
            }
            // Vérifier le format de l'email
            if (!IsValidEmail(createEmployeeWithDepartmentID.Email))
            {
                throw new ArgumentException("Invalid email format.", nameof(createEmployeeWithDepartmentID.Email));
            }

            // Vérifier le format du numéro de téléphone
            if (!IsValidPhoneNumber(createEmployeeWithDepartmentID.Phonenumber))
            {
                throw new ArgumentException("Invalid phone number format.", nameof(createEmployeeWithDepartmentID.Phonenumber));
            }

            if (await _employeeRepository.ExistAsync(createEmployeeWithDepartmentID.Email))
            {
                throw new Exception($"Employee with email {createEmployeeWithDepartmentID.Email} already exists.");
            }
            var department = await _departmentRepository.GetDepartmentByIDAsync(createEmployeeWithDepartmentID.DepartmentID);
            if (!await _departmentRepository.ExistAsync(department.Name))
            {
                throw new Exception($"Department with ID {createEmployeeWithDepartmentID.DepartmentID} not found.");
            }

            var employeeToCreate = _mapper.Map<Employee>(createEmployeeWithDepartmentID);
            employeeToCreate.Departments.Add(department);
            var createdEmployee = await _employeeRepository.CreateEmployeeAsync(employeeToCreate);


            return _mapper.Map<ReadEmployee>(createdEmployee) ?? throw new Exception("Failed to create employee.");
        }

        /// <summary>
        /// Updates the employee asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="updateEmployee">The update employee.</param>
        /// <exception cref="System.ArgumentException">Employee ID should be greater than 0. - employeeId</exception>
        /// <exception cref="System.ArgumentNullException">updateEmployee</exception>
        /// <exception cref="System.Exception">
        /// Employee with ID {employeeId} not found.
        /// or
        /// Employee with email {updateEmployee.Email} already exists.
        /// </exception>
        public async Task UpdateEmployeeAsync(int employeeId, UpdateEmployee updateEmployee)
        {
            if (employeeId <= 0)
            {
                throw new ArgumentException("Employee ID should be greater than 0.", nameof(employeeId));
            }
            // Vérifier le format du numéro de téléphone
            if (!IsValidPhoneNumber(updateEmployee.Phonenumber))
            {
                throw new ArgumentException("Invalid phone number format.", nameof(updateEmployee.Phonenumber));
            }

            if (updateEmployee == null)
            {
                throw new ArgumentNullException(nameof(updateEmployee));
            }

            var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(employeeId)
                ?? throw new Exception($"Employee with ID {employeeId} not found.");

            if (!string.IsNullOrWhiteSpace(updateEmployee.Email) && existingEmployee.Email != updateEmployee.Email && await _employeeRepository.ExistAsync(updateEmployee.Email))
            {
                throw new Exception($"Employee with email {updateEmployee.Email} already exists.");
            }

            _mapper.Map(updateEmployee, existingEmployee);
            await _employeeRepository.UpdateEmployeeAsync(existingEmployee);
        }

        /// <summary>
        /// Deletes the employee by identifier asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <exception cref="System.ArgumentException">Employee ID should be greater than 0. - employeeId</exception>
        /// <exception cref="System.Exception">Employee with ID {employeeId} not found.</exception>
        public async Task DeleteEmployeeByIdAsync(int employeeId)
        {
            if (employeeId <= 0)
            {
                throw new ArgumentException("Employee ID should be greater than 0.", nameof(employeeId));
            }

            var employeeToDelete = await _employeeRepository.GetEmployeeByIdAsync(employeeId)
                ?? throw new Exception($"Employee with ID {employeeId} not found.");

            // Vérifier s'il y a des départements liés à l'employé
            if (employeeToDelete.Departments.Any())
            {
                throw new Exception($"Cannot delete employee with ID {employeeId} because they are associated with departments. Use Delete force EndPoint");
            }

            // Vérifier s'il y a des assiduités (attendances) liées à l'employé
            if (employeeToDelete.Attendances.Any())
            {
                throw new Exception($"Cannot delete employee with ID {employeeId} because they have associated attendances. Use Delete force EndPoint");
            }

            await _employeeRepository.DeleteEmployeeByIdAsync(employeeId);
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Vérifiez que le numéro de téléphone ne dépasse pas 13 caractères
            return !string.IsNullOrEmpty(phoneNumber) && phoneNumber.Length <= 13;
        }
    }
}
