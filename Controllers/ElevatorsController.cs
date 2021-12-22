using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestNew.Models;

namespace RestNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElevatorsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public ElevatorsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Elevators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Elevator>>> Getelevators()
        {
            return await _context.elevators.ToListAsync();
        }

        // GET: api/Elevators/offline
        // get all elevators that are not "Online"
        [HttpGet("offline")]
        public async Task<ActionResult<List<Elevator>>> GetOfflineElevators()
        {
            var elevator = await _context.elevators
                .Where(c => c.status != "Online").ToListAsync();

            if (elevator == null)
            {
                return NotFound();
            }

            return elevator;
        }

        // GET: api/Elevators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Elevator>> GetElevator(int id)
        {
            var elevator = await _context.elevators.FindAsync(id);

            if (elevator == null)
            {
                return NotFound();
            }

            return elevator;
        }

        // GET: api/Elevators/5/status
        // get specific elevator's status
        [HttpGet("{id}/status")]
        public async Task<ActionResult<String>> GetElevatorStatus(int id)
        {
            var elevator = await _context.elevators.FindAsync(id);

            if (elevator == null)
            {
                return NotFound();
            }

            return "Elevator " + elevator.id + "'s status is: " + elevator.status;
        }

        // PUT: api/Elevators/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutElevator(int id, Elevator elevator)
        {
            if (id != elevator.id)
            {
                return BadRequest();
            }

            _context.Entry(elevator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElevatorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Elevators
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Elevator>> PostElevator(Elevator elevator)
        {
            _context.elevators.Add(elevator);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetElevator", new { id = elevator.id }, elevator);
        }

        // DELETE: api/Elevators/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElevator(int id)
        {
            var elevator = await _context.elevators.FindAsync(id);
            if (elevator == null)
            {
                return NotFound();
            }

            _context.elevators.Remove(elevator);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PATCH: api/Elevators/5
        // update status (or any single field) of an elevator using the following format:
        // [{"op": "replace", "path": "/status", "value": "Offline"}]
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchElevatorStatus(int id, [FromBody] JsonPatchDocument<Elevator> elevatorPatch)
        {
            var elevator = await _context.elevators.FindAsync(id);
            elevatorPatch.ApplyTo(elevator);

            await _context.SaveChangesAsync();

            return Content("Successfully updated elevator " + elevator.id);
        }

        private bool ElevatorExists(int id)
        {
            return _context.elevators.Any(e => e.id == id);
        }
    }
}
