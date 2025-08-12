using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeBeanFlowDB.Contexts;
using CoffeBeanFlowDB.Models;

namespace CoffeBeanFlowDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GbxMadurasApiController : ControllerBase
    {
        private readonly Gbx_madurasContext _context;

        public GbxMadurasApiController(Gbx_madurasContext context)
        {
            _context = context;
        }

        // GET: api/GbxMadurasApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gbx_madurasItem>>> GetGbxMaduras()
        {
            return await _context.Gbx_maduras.ToListAsync();
        }

        // GET: api/GbxMadurasApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gbx_madurasItem>> GetGbxMadurasItem(int id)
        {
            var item = await _context.Gbx_maduras.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // POST: api/GbxMadurasApi
        [HttpPost]
        public async Task<ActionResult<Gbx_madurasItem>> PostGbxMadurasItem(Gbx_madurasItem item)
        {
            _context.Gbx_maduras.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGbxMadurasItem), new { id = item.ID_Gbx_maduras }, item);
        }

        // PUT: api/GbxMadurasApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGbxMadurasItem(int id, Gbx_madurasItem item)
        {
            if (id != item.ID_Gbx_maduras)
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
                if (!GbxMadurasItemExists(id))
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

        // DELETE: api/GbxMadurasApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGbxMadurasItem(int id)
        {
            var item = await _context.Gbx_maduras.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Gbx_maduras.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GbxMadurasItemExists(int id)
        {
            return _context.Gbx_maduras.Any(e => e.ID_Gbx_maduras == id);
        }
    }
}
