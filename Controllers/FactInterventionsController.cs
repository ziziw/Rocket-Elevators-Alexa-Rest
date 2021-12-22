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
        private readonly PostgreApplicationContext _context;
        private readonly ApplicationContext _context2;
        public FactInterventionsController(PostgreApplicationContext context, ApplicationContext context2)
        {
            _context = context;
            _context2 = context2;
        }

        // GET: api/FactInterventions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FactIntervention>>> GetFactIntervention()
        {
            return await _context.fact_interventions.ToListAsync();
        }

        // GET: api/FactInterventions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FactIntervention>> GetFactIntervention(int id)
        {
            var factIntervention = await _context.fact_interventions.FindAsync(id);

            if (factIntervention == null)
            {
                return NotFound();
            }

            return factIntervention;
        }

        //GET from specific intervention
        [HttpGet("{id}/bonus1")]
        public async Task<ActionResult<String>> GetSpecificIntervention(int id)
        {
            var factIntervention = await _context.fact_interventions.FindAsync(id);
            var building = await _context2.buildings.FindAsync(factIntervention.building_id);
            var address = await _context2.addresses.FindAsync(building.address_id);
            if (factIntervention == null)
            {
                return NotFound();
            }
            return "factIntervention start intervention:" + factIntervention.start_intervention + "\nfactIntervention stop intervention " + factIntervention.end_intervention + "\naddress: " + address.number_and_street;
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
            _context.fact_interventions.Add(factIntervention);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFactIntervention", new { id = factIntervention.intervention_id }, factIntervention);
        }

        // DELETE: api/FactInterventions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFactIntervention(int id)
        {
            var factIntervention = await _context.fact_interventions.FindAsync(id);
            if (factIntervention == null)
            {
                return NotFound();
            }

            _context.fact_interventions.Remove(factIntervention);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FactInterventionExists(int id)
        {
            return _context.fact_interventions.Any(e => e.intervention_id == id);
        }
    }
}
