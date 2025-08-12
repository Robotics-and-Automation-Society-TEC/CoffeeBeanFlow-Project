using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeBeanFlowDB.Contexts;
using CoffeBeanFlowDB.Models;

namespace CoffeBeanFlowDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RCinmadurasApiController : ControllerBase
    {
        private readonly RCinmadurasContext _context;

        public RCinmadurasApiController(RCinmadurasContext context)
        {
            _context = context;
        }

        // GET: api/RCinmadurasApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RCinmadurasItem>>> GetAll()
        {
            return await _context.RCinmaduras.ToListAsync();
        }

        // GET: api/RCinmadurasApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RCinmadurasItem>> GetById(int id)
        {
            var item = await _context.RCinmaduras.FindAsync(id);
            if (item == null)
                return NotFound();
            return item;
        }

        // POST: api/RCinmadurasApi
        [HttpPost]
        public async Task<ActionResult<RCinmadurasItem>> Create(RCinmadurasItem item)
        {
            _context.RCinmaduras.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = item.ID_inmaduras }, item);
        }

        // PUT: api/RCinmadurasApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RCinmadurasItem item)
        {
            if (id != item.ID_inmaduras)
                return BadRequest();

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RCinmadurasItemExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/RCinmadurasApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.RCinmaduras.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.RCinmaduras.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RCinmadurasItemExists(int id)
        {
            return _context.RCinmaduras.Any(e => e.ID_inmaduras == id);
        }
    }
}
