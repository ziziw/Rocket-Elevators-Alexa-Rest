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
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public CustomersController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Getcustomers()
        {
            return await _context.customers.ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // GET: api/Customers/exist/email
        [HttpGet("exist/{email}")]
        public async Task<ActionResult<String>> GetEmailExist(string email)
        {
            var customers = await _context.customers.ToListAsync();

            foreach (var customer in customers)
            {
                if (customer.email_of_the_company_contact == email)
                {
                    return Ok();
                }
            }

            return NotFound();
        }

        // return the customer with email. 
        // GET: api/Customers/customer/email
        [HttpGet("customer/{email}")]
        public async Task<ActionResult<Customer>> GetCustomerByEmail(string email)
        {
            var customer = await _context.customers.FirstOrDefaultAsync(c => c.email_of_the_company_contact == email);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // return buildings of the customer. 
        // GET: api/Customers/buildings/id
        [HttpGet("buildings/{id}")]
        public async Task<ActionResult<List<Building>>> GetBuildingsOfCustomer(int id)
        {
            var customer = await _context.customers.FirstOrDefaultAsync(c => c.id == id);

            if (customer == null)
            {
                return NotFound();
            }

            var buildings = await _context.buildings.Where(b => b.customer_id == customer.id).ToListAsync();

            return buildings;
        }

        // return batteries of the building. 
        // GET: api/Customers/batteries/id
        [HttpGet("batteries/{id}")]
        public async Task<ActionResult<List<Battery>>> GetBatteriesOfBuilding(int id)
        {
            var batteries = await _context.batteries.Where(b => b.building_id == id).ToListAsync();

            return batteries;
        }

        // return columns of the battery. 
        // GET: api/Customers/columns/id
        [HttpGet("columns/{id}")]
        public async Task<ActionResult<List<Column>>> GetColumnsOfBattery(int id)
        {
            var columns = await _context.columns.Where(c => c.battery_id == id).ToListAsync();

            return columns;
        }

        // return elevators of the column. 
        // GET: api/Customers/elevators/id
        [HttpGet("elevators/{id}")]
        public async Task<ActionResult<List<Elevator>>> GetElevatorsOfColumn(int id)
        {
            var elevators = await _context.elevators.Where(e => e.column_id == id).ToListAsync();

            return elevators;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // PUT: api/Customers
        [HttpPut]
        public async Task<IActionResult> PutCustomerInfo(Customer customer)
        {
            Console.WriteLine("Entered the PUT method");
            var foundCustomer = await _context.customers.FirstOrDefaultAsync(c => c.email_of_the_company_contact == customer.email_of_the_company_contact);

            Console.WriteLine("The customer's email is: " + foundCustomer.email_of_the_company_contact);

            if (foundCustomer == null)
            {
                return NotFound();
            }

            foundCustomer.company_name = customer.company_name;
            foundCustomer.company_headquarters_address = customer.company_headquarters_address;
            foundCustomer.full_name_of_the_company_contact = customer.full_name_of_the_company_contact;
            foundCustomer.company_contact_phone = customer.company_contact_phone;
            foundCustomer.email_of_the_company_contact = customer.email_of_the_company_contact;
            foundCustomer.company_description = customer.company_description;
            foundCustomer.technical_authority_phone_for_service = customer.technical_authority_phone_for_service;
            foundCustomer.technical_manager_email_for_service = customer.technical_manager_email_for_service;
            foundCustomer.full_name_of_service_technical_authority = customer.full_name_of_service_technical_authority;

            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _context.customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.id }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.customers.Any(e => e.id == id);
        }
    }
}
