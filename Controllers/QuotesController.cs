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
    public class QuotesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public QuotesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Quotes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quote>>> Getquotes()
        {
            return await _context.quotes.ToListAsync();
        }

        // GET: api/Quotes/cheapest
        [HttpGet("cheapest")]
        public async Task<ActionResult<String>> GetCheapestQuote()
        {
            var quotes = await _context.quotes.ToListAsync();
            string price = "9999999999";

            foreach (var quote in quotes)
            {
                if (Int64.Parse(quote.total_cost) < Int64.Parse(price))
                {
                    price = quote.total_cost;
                }
            }

            return price;
        }

        // GET: api/Quotes/mostExpensive
        [HttpGet("mostExpensive")]
        public async Task<ActionResult<String>> GetMostExpensiveQuote()
        {
            var quotes = await _context.quotes.ToListAsync();
            string price = "0";

            foreach (var quote in quotes)
            {
                if (Int64.Parse(quote.total_cost) > Int64.Parse(price))
                {
                    price = quote.total_cost;
                }
            }

            return price;
        }

        // GET: api/Quotes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Quote>> GetQuote(int id)
        {
            var quote = await _context.quotes.FindAsync(id);

            if (quote == null)
            {
                return NotFound();
            }

            return quote;
        }

        // PUT: api/Quotes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuote(int id, Quote quote)
        {
            if (id != quote.id)
            {
                return BadRequest();
            }

            _context.Entry(quote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuoteExists(id))
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

        // POST: api/Quotes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Quote>> PostQuote(Quote quote)
        {
            _context.quotes.Add(quote);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuote", new { id = quote.id }, quote);
        }

        // DELETE: api/Quotes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuote(int id)
        {
            var quote = await _context.quotes.FindAsync(id);
            if (quote == null)
            {
                return NotFound();
            }

            _context.quotes.Remove(quote);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuoteExists(int id)
        {
            return _context.quotes.Any(e => e.id == id);
        }
    }
}
