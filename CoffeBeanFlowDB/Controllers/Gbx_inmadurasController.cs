using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeBeanFlowDB.Contexts;
using CoffeBeanFlowDB.Models;

namespace CoffeBeanFlowDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GbxInmadurasApiController : ControllerBase
    {
        private readonly Gbx_inmadurasContext _context;

        public GbxInmadurasApiController(Gbx_inmadurasContext context)
        {
            _context = context;
        }

        // GET: api/GbxInmadurasApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gbx_inmadurasItem>>> GetGbxInmaduras()
        {
            return await _context.Gbx_inmaduras.ToListAsync();
        }

        // GET: api/GbxInmadurasApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gbx_inmadurasItem>> GetGbxInmadurasItem(int id)
        {
            var item = await _context.Gbx_inmaduras.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // POST: api/GbxInmadurasApi
        [HttpPost]
        public async Task<ActionResult<Gbx_inmadurasItem>> PostGbxInmadurasItem(Gbx_inmadurasItem item)
        {
            _context.Gbx_inmaduras.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGbxInmadurasItem), new { id = item.ID_Gbx_inmaduras }, item);
        }

        // PUT: api/GbxInmadurasApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGbxInmadurasItem(int id, Gbx_inmadurasItem item)
        {
            if (id != item.ID_Gbx_inmaduras)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GbxInmadurasItemExists(id))
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

        // DELETE: api/GbxInmadurasApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGbxInmadurasItem(int id)
        {
            var item = await _context.Gbx_inmaduras.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Gbx_inmaduras.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GbxInmadurasItemExists(int id)
        {
            return _context.Gbx_inmaduras.Any(e => e.ID_Gbx_inmaduras == id);
        }
    }
}
