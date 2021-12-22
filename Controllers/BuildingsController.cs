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
    public class BuildingsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly PostgreApplicationContext _context2;

        public BuildingsController(ApplicationContext context, PostgreApplicationContext context2)
        {
            _context = context;
            _context2 = context2;
        }

        // GET: api/Buildings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Building>>> Getbuildings()
        {
            return await _context.buildings.ToListAsync();
        }

        // GET: api/Buildings/intervention
        // find buildings with intervention
        [HttpGet("intervention")]
        public async Task<ActionResult<IEnumerable<Building>>> GetBuildingsIntervention()
        {
            var findBuildings = (from buildings in _context.buildings
                                 join batteries in _context.batteries on buildings.id equals batteries.building_id
                                 join columns in _context.columns on batteries.id equals columns.battery_id
                                 join elevators in _context.elevators on columns.id equals elevators.column_id
                                 where elevators.status == "Intervention" || columns.status == "Intervention" || batteries.status == "Intervention"
                                 select buildings).Distinct();
            return await findBuildings.ToListAsync();
        }

        // GET: api/Buildings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Building>> GetBuilding(int id)
        {
            var building = await _context.buildings.FindAsync(id);

            if (building == null)
            {
                return NotFound();
            }

            return building;
        }

        // GET: api/Buildings/bonus2
        // 2nd query in graphql done here
        [HttpGet("{id}/bonus2")]
        public async Task<ActionResult<string>> getInterventionForBuilding(int id)
        {
            var building = await _context.buildings.FindAsync(id);

            var factInterventions = _context2.fact_interventions.Where(f => f.building_id == building.id).ToListAsync();
            var customer = await _context.customers.FindAsync(id);
            var returnString = "";
            foreach (var factIntervention in await factInterventions)
            {
                returnString = returnString + factIntervention.ToString() + "\n";
            }
            return returnString + "Customer ID: " + customer.id;

        }

        // PUT: api/Buildings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuilding(int id, Building building)
        {
            if (id != building.id)
            {
                return BadRequest();
            }

            _context.Entry(building).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildingExists(id))
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

        // POST: api/Buildings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Building>> PostBuilding(Building building)
        {
            _context.buildings.Add(building);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuilding", new { id = building.id }, building);
        }

        // DELETE: api/Buildings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuilding(int id)
        {
            var building = await _context.buildings.FindAsync(id);
            if (building == null)
            {
                return NotFound();
            }

            _context.buildings.Remove(building);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BuildingExists(int id)
        {
            return _context.buildings.Any(e => e.id == id);
        }
    }
}
