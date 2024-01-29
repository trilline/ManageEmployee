using ManageEmployees.Dtos.Attendance;
using ManageEmployees.Dtos.Statusleaverequest;
using ManageEmployees.Entities;
using ManageEmployees.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManageEmployees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusLeaveRequestController : ControllerBase
    {
        private readonly IStatusLeaveRequestService _statusLeaveRequestService;

        public StatusLeaveRequestController(IStatusLeaveRequestService statusLeaveRequestService)
        {
            _statusLeaveRequestService = statusLeaveRequestService ?? throw new ArgumentNullException(nameof(statusLeaveRequestService));
        }

        // GET: api/<StatusLeaveRequestController>
        [HttpGet]
        public async Task<ActionResult<List<ReadStatusLeaveRequest>>> GetStatusLeaveRequests()
        {
            try
            {
                var statusLeaveRequests = await _statusLeaveRequestService.GetStatusLeaveRequests();
                return Ok(statusLeaveRequests);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GET api/<StatusLeaveRequestController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadStatusLeaveRequest>> GetStatusLeaveRequestById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("L'identifiant du statut de demande de congé doit être supérieur à zéro.");
                }

                var statusLeaveRequest = await _statusLeaveRequestService.GetStatusLeaveRequestByIdAsync(id);
                if (statusLeaveRequest == null )
                {
                    return NotFound("Assiduité non trouvée.");
                }
                return Ok(statusLeaveRequest);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // POST api/<StatusLeaveRequestController>
        [HttpPost]
        public async Task<ActionResult<ReadStatusLeaveRequest>> PostStatusLeaveRequest([FromBody] CreateStatusLeaveRequest createStatusLeaveRequest)
        {
            try
            {
                if (createStatusLeaveRequest == null || string.IsNullOrWhiteSpace(createStatusLeaveRequest.Statuslabel))
                {
                    return BadRequest("Les informations pour créer un statut de demande de congé sont nulles.");
                }

                var createdStatusLeaveRequest = await _statusLeaveRequestService.CreateStatusLeaveRequestAsync(createStatusLeaveRequest);
                return Ok(createdStatusLeaveRequest);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // PUT api/<StatusLeaveRequestController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutStatusLeaveRequest(int id, [FromBody] UpdateStatusLeaveRequest updateStatusLeaveRequest)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("L'identifiant du statut de demande de congé doit être supérieur à zéro.");
                }

                if (updateStatusLeaveRequest == null || string.IsNullOrWhiteSpace(updateStatusLeaveRequest.Statuslabel))
                {
                    return BadRequest("Les informations pour mettre à jour le statut de demande de congé sont nulles.");
                }

                await _statusLeaveRequestService.UpdateStatusLeaveRequestAsync(id, updateStatusLeaveRequest);
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // DELETE api/<StatusLeaveRequestController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStatusLeaveRequest(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("L'identifiant du statut de demande de congé doit être supérieur à zéro.");
                }

                await _statusLeaveRequestService.DeleteStatusLeaveRequestByIdAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
