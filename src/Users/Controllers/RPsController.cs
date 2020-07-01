using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Users.API.Models;
using Users.API.Models.Context;

namespace Users.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RPsController : ControllerBase
    {
        private readonly dbContext _context;

        public RPsController(dbContext context)
        {
            _context = context;
        }

        // GET: api/RPs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RP>>> GetRPs()
        {
            return await _context.RPs.ToListAsync();
        }

        // GET: api/RPs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RP>> GetRP(int id)
        {
            var rP = await _context.RPs.FindAsync(id);

            if (rP == null)
            {
                return NotFound();
            }

            return rP;
        }

        // PUT: api/RPs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRP(int id, RP rP)
        {
            if (id != rP.Id)
            {
                return BadRequest();
            }

            _context.Entry(rP).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RPExists(id))
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

        // POST: api/RPs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<RP>> PostRP(RP rP)
        {
            _context.RPs.Add(rP);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRP", new { id = rP.Id }, rP);
        }

        // DELETE: api/RPs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RP>> DeleteRP(int id)
        {
            var rP = await _context.RPs.FindAsync(id);
            if (rP == null)
            {
                return NotFound();
            }

            _context.RPs.Remove(rP);
            await _context.SaveChangesAsync();

            return rP;
        }

        private bool RPExists(int id)
        {
            return _context.RPs.Any(e => e.Id == id);
        }
    }
}
