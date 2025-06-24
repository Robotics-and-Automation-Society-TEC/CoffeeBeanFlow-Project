using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeBeanFlowDB.Contexts;
using CoffeBeanFlowDB.Models;

namespace CoffeBeanFlowDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RCsobremadurasApiController : ControllerBase
    {
        private readonly RCsobremadurasContext _context;

        public RCsobremadurasApiController(RCsobremadurasContext context)
        {
            _context = context;
        }

        // GET: api/RCsobremadurasApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RCsobremadurasItem>>> GetAll()
        {
            return await _context.RCsobremaduras.ToListAsync();
        }

        // GET: api/RCsobremadurasApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RCsobremadurasItem>> GetById(int id)
        {
            var item = await _context.RCsobremaduras.FindAsync(id);
            if (item == null)
                return NotFound();
            return item;
        }

        // POST: api/RCsobremadurasApi
        [HttpPost]
        public async Task<ActionResult<RCsobremadurasItem>> Create(RCsobremadurasItem item)
        {
            _context.RCsobremaduras.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = item.ID_sobremaduras }, item);
        }

        // PUT: api/RCsobremadurasApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RCsobremadurasItem item)
        {
            if (id != item.ID_sobremaduras)
                return BadRequest();

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RCsobremadurasItemExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/RCsobremadurasApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.RCsobremaduras.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.RCsobremaduras.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RCsobremadurasItemExists(int id)
        {
            return _context.RCsobremaduras.Any(e => e.ID_sobremaduras == id);
        }
    }
}
