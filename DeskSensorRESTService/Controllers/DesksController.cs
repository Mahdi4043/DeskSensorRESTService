using Microsoft.AspNetCore.Mvc;
using DeskSensorRESTService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
// Test af ymal
namespace DeskSensorRESTService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesksController : ControllerBase
    {
        private readonly DeskRepoDb _desks;

        public DesksController(DeskRepoDb desks)
        {
            _desks = desks;
        }

        // GET: api/<PlayGroundsController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Desk> desks = _desks.Get();
            if (desks == null || !desks.Any())
            {
                return NotFound("No desks found.");
            }
            return Ok(desks);
        }

        // GET api/<PlayGroundsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<Desk> GetId(int id)
        {
            Desk? desk = _desks.GetById(id);
            if (desk == null) return NotFound("No such desk, id: " + id);
            return Ok(desk);
        }


        // POST api/<PlayGroundsController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public IActionResult Post([FromBody] Desk desk)
        {
            if (desk.Occupied == null)
            {
                return BadRequest("Value cannot be null or empty.");
            }
            _desks.Add(desk);
            return Created("/" + desk.Id, desk);
        }

        // PUT api/<PlayGroundsController>/5
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Desk desk)
        {
            if (desk == null || desk.Id != id)
            {
                return BadRequest("Desk object is null or ID mismatch.");
            }

            // Attempt to update the PlayGround
            Desk? updatedDesk = _desks.Update(id, desk);

            if (updatedDesk == null)
            {
                return NotFound($"Desk with ID {id} not found.");
            }

            // Return NoContent (204) when the update is successful
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id) 
        {   
            if (id != null)
            {
                var Desk = _desks.GetById(id);
                _desks.Delete(Desk.Id);
                return Ok(Desk);
            }

            return NotFound($"Desk with ID {id} not found.");
        }
    }
}
