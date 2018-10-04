using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeberPage.Models;

namespace WeberPage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftsController : ControllerBase
    {
        private readonly WeberPageContext _context;

        public ShiftsController(WeberPageContext context)
        {
            _context = context;
        }

        // GET: api/Shifts
        [HttpGet]
        public IEnumerable<Shift> GetShift()
        {
            return _context.Shift;
        }

        // GET: api/Shifts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShift([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shift = await _context.Shift.FindAsync(id);

            if (shift == null)
            {
                return NotFound();
            }

            return Ok(shift);
        }

        // PUT: api/Shifts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShift([FromRoute] int id, [FromBody] Shift shift)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shift.Id)
            {
                return BadRequest();
            }

            _context.Entry(shift).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShiftExists(id))
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

        // POST: api/Shifts
        [HttpPost]
        public async Task<IActionResult> PostShift([FromBody] Shift shift)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Shift.Add(shift);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShift", new { id = shift.Id }, shift);
        }

        // DELETE: api/Shifts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShift([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shift = await _context.Shift.FindAsync(id);
            if (shift == null)
            {
                return NotFound();
            }

            _context.Shift.Remove(shift);
            await _context.SaveChangesAsync();

            return Ok(shift);
        }

        private bool ShiftExists(int id)
        {
            return _context.Shift.Any(e => e.Id == id);
        }
    }
}