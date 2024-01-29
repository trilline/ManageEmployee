using ManageEmployees.Dtos.Leaverequest;
using ManageEmployees.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

// ... (autres directives using)

namespace ManageEmployees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly ILeaveRequestService _leaveRequestService;

        public LeaveRequestController(ILeaveRequestService leaveRequestService)
        {
            _leaveRequestService = leaveRequestService;
        }

        // GET: api/<LeaveRequestController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadLeaveRequest>>> GetLeaveRequests()
        {
            try
            {
                var leaveRequests = await _leaveRequestService.GetLeaveRequests();
                return Ok(leaveRequests);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GET api/<LeaveRequestController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadLeaveRequest>> GetLeaveRequest(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("ID doit être supérieur à zéro.");
                }
                var leaveRequest = await _leaveRequestService.GetLeaveRequestByIdAsync(id);

                if (leaveRequest == null)
                    return NotFound($"Le Leave Request avec l'ID {id} n'existe pas.");

                return Ok(leaveRequest);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // POST api/<LeaveRequestController>
        [HttpPost]
        public async Task<ActionResult<ReadLeaveRequest>> PostLeaveRequest([FromBody] CreateLeaveRequest createLeaveRequest)
        {
            try
            {
                if (createLeaveRequest == null || string.IsNullOrWhiteSpace(createLeaveRequest.StartDate.ToShortDateString()) || string.IsNullOrWhiteSpace(createLeaveRequest.EndDate.ToShortDateString()))
                {
                    return BadRequest("Les informations de Leave Request sont nulles ou vides.");
                }

                var createdLeaveRequest = await _leaveRequestService.CreateLeaveRequestAsync(createLeaveRequest);
                return Ok(createdLeaveRequest);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // PUT api/<LeaveRequestController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutLeaveRequest(int id, [FromBody] UpdateLeaveRequest updateLeaveRequest)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("ID doit être supérieur à zéro.");
                }

                if (updateLeaveRequest == null || string.IsNullOrWhiteSpace(updateLeaveRequest.StartDate.ToShortDateString()) || string.IsNullOrWhiteSpace(updateLeaveRequest.EndDate.ToShortDateString()))
                {
                    return BadRequest("Les informations de Leave Request sont nulles ou vides.");
                }

                var leaveRequestExists = await _leaveRequestService.LeaveRequestExistsAsync(id);
                if (!leaveRequestExists)
                    return NotFound($"Le Leave Request avec l'ID {id} n'existe pas.");

                await _leaveRequestService.UpdateLeaveRequestAsync(id, updateLeaveRequest);
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // DELETE api/<LeaveRequestController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLeaveRequest(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("ID doit être supérieur à zéro.");
                }
                var leaveRequestExists = await _leaveRequestService.LeaveRequestExistsAsync(id);
                if (!leaveRequestExists)
                    return NotFound($"Le Leave Request avec l'ID {id} n'existe pas.");

                await _leaveRequestService.DeleteLeaveRequestByIdAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

