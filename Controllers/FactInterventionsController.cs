using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestNew.Models;

namespace RestNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactInterventionsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public FactInterventionsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/FactInterventions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FactIntervention>>> GetFactIntervention()
        {
            return await _context.FactIntervention.ToListAsync();
        }

        // GET: api/FactInterventions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FactIntervention>> GetFactIntervention(int id)
        {
            var factIntervention = await _context.FactIntervention.FindAsync(id);

            if (factIntervention == null)
            {
                return NotFound();
            }

            return factIntervention;
        }

        // PUT: api/FactInterventions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFactIntervention(int id, FactIntervention factIntervention)
        {
            if (id != factIntervention.intervention_id)
            {
                return BadRequest();
            }

            _context.Entry(factIntervention).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FactInterventionExists(id))
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

        // POST: api/FactInterventions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FactIntervention>> PostFactIntervention(FactIntervention factIntervention)
        {
            _context.FactIntervention.Add(factIntervention);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFactIntervention", new { id = factIntervention.intervention_id }, factIntervention);
        }

        // DELETE: api/FactInterventions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFactIntervention(int id)
        {
            var factIntervention = await _context.FactIntervention.FindAsync(id);
            if (factIntervention == null)
            {
                return NotFound();
            }

            _context.FactIntervention.Remove(factIntervention);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FactInterventionExists(int id)
        {
            return _context.FactIntervention.Any(e => e.intervention_id == id);
        }
    }
}
