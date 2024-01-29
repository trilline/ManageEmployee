using ManageEmployees.Dtos.Attendance;
using ManageEmployees.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManageEmployees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        // GET: api/<AttendanceController>
        [HttpGet]
        public async Task<ActionResult<List<ReadAttendance>>> GetAttendancesAsync()
        {
            try
            {
                var attendances = await _attendanceService.GetAttendancesAsync();
                return Ok(attendances);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GET api/<AttendanceController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadAttendance>> GetAttendanceByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("ID doit être supérieur à zéro.");
                }

                var attendance = await _attendanceService.GetAttendanceByIdAsync(id);

                if (attendance == null)
                {
                    return NotFound("Assiduité non trouvée.");
                }

                return Ok(attendance);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // POST api/<AttendanceController>
        [HttpPost]
        public async Task<ActionResult<ReadAttendance>> Post([FromBody] CreateAttendance createAttendance)
        {
            try
            {
                if (createAttendance == null || string.IsNullOrWhiteSpace(createAttendance.EmployeeId.ToString()))
                {
                    return BadRequest("Données d'assiduité invalides.");
                }

                var createdAttendance = await _attendanceService.CreateAttendanceAsync(createAttendance);
                return Ok(createdAttendance);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // PUT api/<AttendanceController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateAttendance updateAttendance)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("ID doit être supérieur à zéro.");
                }

                if (updateAttendance == null || string.IsNullOrWhiteSpace(updateAttendance.EmployeeId.ToString()))
                {
                    return BadRequest("Données d'assiduité invalides.");
                }

                await _attendanceService.UpdateAttendanceAsync(id, updateAttendance);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // DELETE api/<AttendanceController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("ID doit être supérieur à zéro.");
                }

                await _attendanceService.DeleteAttendanceByIdAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
