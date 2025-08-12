using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeBeanFlowDB.Contexts;
using CoffeBeanFlowDB.Models;


namespace CoffeBeanFlowDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuministraApiController : ControllerBase
    {
        private readonly SuministraContext _context;

        public SuministraApiController(SuministraContext context)
        {
            _context = context;
        }

        // GET: api/SuministraApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SuministraItem>>> GetAll()
        {
            return await _context.Suministra.ToListAsync();
        }

        // GET: api/SuministraApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SuministraItem>> GetById(int id)
        {
            var item = await _context.Suministra.FindAsync(id);
            if (item == null)
                return NotFound();

            return item;
        }

        // POST: api/SuministraApi
        [HttpPost]
        public async Task<ActionResult<SuministraItem>> Create(SuministraItem item)
        {
            _context.Suministra.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = item.ID_Bodega }, item);
        }

        // PUT: api/SuministraApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SuministraItem item)
        {
            if (id != item.ID_Bodega)
                return BadRequest();

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuministraItemExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/SuministraApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Suministra.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.Suministra.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SuministraItemExists(int id)
        {
            return _context.Suministra.Any(e => e.ID_Bodega == id);
        }
    }
}
