using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeBeanFlowDB.Contexts;
using CoffeBeanFlowDB.Models;

namespace CoffeBeanFlowDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatacionApiController : ControllerBase
    {
        private readonly CatacionContext _context;

        public CatacionApiController(CatacionContext context)
        {
            _context = context;
        }

        // GET: api/CatacionApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatacionItem>>> GetCatacion()
        {
            return await _context.Catacion.ToListAsync();
        }

        // GET: api/CatacionApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CatacionItem>> GetCatacionItem(int id)
        {
            var catacionItem = await _context.Catacion.FindAsync(id);

            if (catacionItem == null)
            {
                return NotFound();
            }

            return catacionItem;
        }

        // POST: api/CatacionApi
        [HttpPost]
        public async Task<ActionResult<CatacionItem>> PostCatacionItem(CatacionItem catacionItem)
        {
            _context.Catacion.Add(catacionItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCatacionItem), new { id = catacionItem.ID_catacion }, catacionItem);
        }

        // PUT: api/CatacionApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCatacionItem(int id, CatacionItem catacionItem)
        {
            if (id != catacionItem.ID_catacion)
            {
                return BadRequest();
            }

            _context.Entry(catacionItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatacionItemExists(id))
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

        // DELETE: api/CatacionApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatacionItem(int id)
        {
            var catacionItem = await _context.Catacion.FindAsync(id);
            if (catacionItem == null)
            {
                return NotFound();
            }

            _context.Catacion.Remove(catacionItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CatacionItemExists(int id)
        {
            return _context.Catacion.Any(e => e.ID_catacion == id);
        }
    }
}
