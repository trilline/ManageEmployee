using AutoMapper;
using ManageEmployees.Dtos.Department;
using ManageEmployees.Entities;
using ManageEmployees.Services;
using ManageEmployees.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManageEmployees.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departementService;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentService departmentService, IMapper mapper)
        {
            _departementService = departmentService;
            _mapper = mapper;
        }


        // GET: api/<DepartmentController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadDepartment>>> Get()
        {
            try
            {
                var departments = await _departementService.GetDepartments();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GET api/<DepartmentController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> Get(int id)
        {
            if (id <1)
            {
                return BadRequest("Echec de récupération d'un departement : les informations sont null ou vides");
            }
            try
            {
                var department = await _departementService.GetDepartmentByIdAsync(id);
                if (department == null)
                {
                    return NotFound($"Aucun département trouvé avec l'identifiant {id}");
                }
                return Ok(department);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GET api/<DepartmentController>/5
        //[HttpGet("{name}")]
        //public async Task<ActionResult<ReadDepartment>> GetbyName(string name)
        //{
        //    if (string.IsNullOrWhiteSpace(name))
        //    {
        //        return BadRequest("Echec de récupération d'un departement : les informations sont null ou vides");
        //    }
        //    try
        //    {
        //        var department = await _departementService.GetDepartmentByNameAsync(name);
        //        if (department == null)
        //        {
        //            return NotFound($"Aucun département trouvé avec le nom {name}");
        //        }
        //        return Ok(department);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Problem(ex.Message);
        //    }
        //}

        // POST api/<DepartmentController>
        [HttpPost]
        public async Task<ActionResult<ReadDepartment>> Post([FromBody] CreateDepartment department)
        {
            if (department == null || string.IsNullOrWhiteSpace(department.Name)
    || string.IsNullOrWhiteSpace(department.Address) || string.IsNullOrWhiteSpace(department.Description))
            {
                return BadRequest("Echec de création d'un departement : les informations sont null ou vides");
            }

            try
            {
                var departmentCreated = await _departementService.CreateDepartmentAsync(department);
                return Ok(departmentCreated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erreur lors de la création du département.", Error = ex.Message });
            }

        }
        // POST api/<DepartmentController>/multiple
        [HttpPost("multiple")] // ou [HttpPost("departments")] 
        public async Task<ActionResult<List<ReadDepartment>>> PostMultiple([FromBody] List<CreateDepartment> departments)
        {
            if (departments == null || departments.Any(d => string.IsNullOrWhiteSpace(d.Name) || string.IsNullOrWhiteSpace(d.Address) || string.IsNullOrWhiteSpace(d.Description)))
            {
                return BadRequest("Echec de création des départements : les informations sont null ou vides pour au moins un département.");
            }

            try
            {
                var createdDepartments = new List<ReadDepartment>();

                foreach (var department in departments)
                {
                    var departmentCreated = await _departementService.CreateDepartmentAsync(department);
                    createdDepartments.Add(new ReadDepartment
                    {
                        Id = departmentCreated.Id,
                        Name = departmentCreated.Name,
                    });
                }

                return Ok(createdDepartments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erreur lors de la création des départements.", Error = ex.Message });
            }
        }
        // PUT api/<DepartmentController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ReadDepartment>> Put(int id, [FromBody] UpdateDepartment updatedDepartment)
        {
            try
            {
                if (updatedDepartment == null || string.IsNullOrWhiteSpace(updatedDepartment.Name)
                    || string.IsNullOrWhiteSpace(updatedDepartment.Address) || string.IsNullOrWhiteSpace(updatedDepartment.Description))
                {
                    return BadRequest("Echec de mise à jour d'un département : les informations sont null ou vides");
                }

                await _departementService.UpdateDepartmentAsync(id, updatedDepartment);

                var department = await _departementService.GetDepartmentByIdAsync(id);
                var updatedDepartmentDetails = _mapper.Map<ReadDepartment>(department);
                return Ok(updatedDepartmentDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Erreur lors de la mise à jour du département avec l'identifiant {id}.", Error = ex.Message });
            }
        }


        // DELETE api/<DepartmentController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("L'identifiant du département est invalide.");
                }
                await _departementService.DeleteDepartmentByIdAsync(id);
                return NoContent(); // Indique que la suppression a réussi
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Erreur lors de la suppression du département avec l'identifiant {id}.", Error = ex.Message });
            }
        }
    }
}
