using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeBeanFlowDB.Contexts;
using CoffeBeanFlowDB.Models;

namespace CoffeBeanFlowDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RCmadurasApiController : ControllerBase
    {
        private readonly RCmadurasContext _context;

        public RCmadurasApiController(RCmadurasContext context)
        {
            _context = context;
        }

        // GET: api/RCmadurasApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RCmadurasItem>>> GetAll()
        {
            return await _context.RCmaduras.ToListAsync();
        }

        // GET: api/RCmadurasApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RCmadurasItem>> GetById(int id)
        {
            var item = await _context.RCmaduras.FindAsync(id);
            if (item == null)
                return NotFound();
            return item;
        }

        // POST: api/RCmadurasApi
        [HttpPost]
        public async Task<ActionResult<RCmadurasItem>> Create(RCmadurasItem item)
        {
            _context.RCmaduras.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = item.ID_maduras }, item);
        }

        // PUT: api/RCmadurasApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RCmadurasItem item)
        {
            if (id != item.ID_maduras)
                return BadRequest();

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RCmadurasItemExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/RCmadurasApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.RCmaduras.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.RCmaduras.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RCmadurasItemExists(int id)
        {
            return _context.RCmaduras.Any(e => e.ID_maduras == id);
        }
    }
}
