using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeBeanFlowDB.Contexts;
using CoffeBeanFlowDB.Models;

namespace CoffeBeanFlowDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodegaApiController : ControllerBase
    {
        private readonly BodegaContext _context;

        public BodegaApiController(BodegaContext context)
        {
            _context = context;
        }

        // GET: api/BodegaApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BodegaItem>>> GetBodega()
        {
            return await _context.Bodega.ToListAsync();
        }

        // GET: api/BodegaApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BodegaItem>> GetBodegaItem(int id)
        {
            var bodegaItem = await _context.Bodega.FindAsync(id);

            if (bodegaItem == null)
            {
                return NotFound();
            }

            return bodegaItem;
        }

        // POST: api/BodegaApi
        [HttpPost]
        public async Task<ActionResult<BodegaItem>> PostBodegaItem(BodegaItem bodegaItem)
        {
            _context.Bodega.Add(bodegaItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBodegaItem), new { id = bodegaItem.ID_Bodega }, bodegaItem);
        }

        // PUT: api/BodegaApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBodegaItem(int id, BodegaItem bodegaItem)
        {
            if (id != bodegaItem.ID_Bodega)
            {
                return BadRequest();
            }

            _context.Entry(bodegaItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BodegaItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // 204
        }

        // DELETE: api/BodegaApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBodegaItem(int id)
        {
            var bodegaItem = await _context.Bodega.FindAsync(id);
            if (bodegaItem == null)
            {
                return NotFound();
            }

            _context.Bodega.Remove(bodegaItem);
            await _context.SaveChangesAsync();

            return NoContent(); // 204
        }

        private bool BodegaItemExists(int id)
        {
            return _context.Bodega.Any(e => e.ID_Bodega == id);
        }
    }
}
