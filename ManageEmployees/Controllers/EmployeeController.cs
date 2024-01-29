using ManageEmployees.Dtos.Employee;
using ManageEmployees.Entities;
using ManageEmployees.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManageEmployees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task<ActionResult<List<ReadEmployee>>> GetEmployees()
        {
            try
            {
                var employees = await _employeeService.GetEmployeesAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadEmployee>> GetEmployee(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                if (employee == null)
                    return NotFound($"Aucun employé trouvé avec l'identifiant {id}");

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // POST api/<EmployeeController>
        [HttpPost("CreateEmployee")]
        public async Task<ActionResult<ReadEmployee>> PostEmployee([FromBody] CreateEmployee createEmployee)
        {
            try
            {
                if (createEmployee == null)
                    return BadRequest("Les informations de l'employé sont nulles.");

                var createdEmployee = await _employeeService.CreateEmployeeAsync(createEmployee);
                return Ok(createdEmployee);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost("CreateEmployeeWithDepartmentID")]
        public async Task<ActionResult<ReadEmployee>> PostEmployeeWithDepartmentID([FromBody] CreateEmployeeWithDepartmentID createEmployeeWithDepartmentID)
        {
            try
            {
                if (createEmployeeWithDepartmentID == null || string.IsNullOrWhiteSpace(createEmployeeWithDepartmentID.Position)
            ||          string.IsNullOrWhiteSpace(createEmployeeWithDepartmentID.Firstname) || string.IsNullOrWhiteSpace(createEmployeeWithDepartmentID.Lastname) || createEmployeeWithDepartmentID.DepartmentID <=0)
                    return BadRequest("L'une des informations de l'employé est nulle.");

                var createdEmployee = await _employeeService.CreateEmployeeAsync(createEmployeeWithDepartmentID);
                return Ok(createdEmployee);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutEmployee(int id, [FromBody] UpdateEmployee updateEmployee)
        {
            try
            {
                if (updateEmployee == null || string.IsNullOrWhiteSpace(updateEmployee.Position)
            || string.IsNullOrWhiteSpace(updateEmployee.Firstname) || string.IsNullOrWhiteSpace(updateEmployee.Lastname))
                return BadRequest("Les informations de mise à jour de l'employé sont nulles.");

                await _employeeService.UpdateEmployeeAsync(id, updateEmployee);
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            try
            {
                await _employeeService.DeleteEmployeeByIdAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
