using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        // GET: api/Elevators/status/offline
        // get all elevators that are not "Online"
        [HttpGet("status/{status}")]
        public async Task<ActionResult<List<Elevator>>> GetElevatorsStatus(string status)
        {
            var elevators = await _context.elevators
                .Where(e => e.status == status).ToListAsync();

            return elevators;
        }

        // GET: api/Elevators/model/standard
        // get all elevators that are standard
        [HttpGet("model/{model}")]
        public async Task<ActionResult<List<Elevator>>> GetElevatorsModel(string model)
        {
            var elevators = await _context.elevators
                .Where(e => e.model == model).ToListAsync();

            return elevators;
        }

        // GET: api/Elevators/type/residential
        // get all elevators that are residential
        [HttpGet("type/{type}")]
        public async Task<ActionResult<List<Elevator>>> GetElevatorsType(string type)
        {
            var elevators = await _context.elevators
                .Where(e => e.elevator_type == type).ToListAsync();

            return elevators;
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

            return "Elevator " + id + "'s status is: " + elevator.status;
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

        //PUT: api/elevators/1/status/
        // need to pass an Elevator JSON
        [HttpPut("{id}/status")]
        public async Task<ActionResult> PutElevatorStatus1(int id, Elevator elevator)
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

            return Content("Elevator " + id + " status changed to " + elevator.status);

        }


        // PUT: api/elevators/1/status/online
        // PUT: api/elevators/1/status/offline
        [HttpPut("{id}/status/{status}")]
        public async Task<ActionResult> PutElevatorStatus2(int id, string status)
        {
            var elevator = await _context.elevators.FindAsync(id);

            if (status == "online")
            {
                elevator.status = "Online";
                await _context.SaveChangesAsync();
                return Content("Elevator " + id + " status changed to Online");
            }
            else if (status == "offline")
            {
                elevator.status = "Offline";
                await _context.SaveChangesAsync();
                return Content("Elevator " + id + " status changed to Offline");
            }
            else
            {
                return Content("Status not valid!");
            }
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
