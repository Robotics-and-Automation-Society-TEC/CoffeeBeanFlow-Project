using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeBeanFlowDB.Contexts;
using CoffeBeanFlowDB.Models;

namespace CoffeBeanFlowDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PesoVerdeApiController : ControllerBase
    {
        private readonly PesoVerdeContext _context;

        public PesoVerdeApiController(PesoVerdeContext context)
        {
            _context = context;
        }

        // GET: api/PesoVerdeApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PesoVerdeItem>>> GetAll()
        {
            return await _context.PesoVerde.ToListAsync();
        }

        // GET: api/PesoVerdeApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PesoVerdeItem>> GetById(int id)
        {
            var item = await _context.PesoVerde.FindAsync(id);

            if (item == null)
                return NotFound();

            return item;
        }

        // POST: api/PesoVerdeApi
        [HttpPost]
        public async Task<ActionResult<PesoVerdeItem>> Create(PesoVerdeItem item)
        {
            _context.PesoVerde.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = item.ID_PesoVerde }, item);
        }

        // PUT: api/PesoVerdeApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PesoVerdeItem item)
        {
            if (id != item.ID_PesoVerde)
                return BadRequest();

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PesoVerdeItemExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/PesoVerdeApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.PesoVerde.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.PesoVerde.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PesoVerdeItemExists(int id)
        {
            return _context.PesoVerde.Any(e => e.ID_PesoVerde == id);
        }
    }
}
