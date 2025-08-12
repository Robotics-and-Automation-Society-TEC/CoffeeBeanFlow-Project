using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeBeanFlowDB.Contexts;
using CoffeBeanFlowDB.Models;


namespace CoffeBeanFlowDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrillaApiController : ControllerBase
    {
        private readonly TrillaContext _context;

        public TrillaApiController(TrillaContext context)
        {
            _context = context;
        }

        // GET: api/TrillaApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrillaItem>>> GetAll()
        {
            return await _context.Trilla.ToListAsync();
        }

        // GET: api/TrillaApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrillaItem>> GetById(int id)
        {
            var item = await _context.Trilla.FindAsync(id);
            if (item == null)
                return NotFound();

            return item;
        }

        // POST: api/TrillaApi
        [HttpPost]
        public async Task<ActionResult<TrillaItem>> Create(TrillaItem item)
        {
            _context.Trilla.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = item.ID_Trilla }, item);
        }

        // PUT: api/TrillaApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TrillaItem item)
        {
            if (id != item.ID_Trilla)
                return BadRequest();

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrillaItemExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/TrillaApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Trilla.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.Trilla.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrillaItemExists(int id)
        {
            return _context.Trilla.Any(e => e.ID_Trilla == id);
        }
    }
}
