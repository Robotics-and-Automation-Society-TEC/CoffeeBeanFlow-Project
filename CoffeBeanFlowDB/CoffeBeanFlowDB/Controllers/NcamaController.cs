using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeBeanFlowDB.Contexts;
using CoffeBeanFlowDB.Models;

namespace CoffeBeanFlowDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NcamaApiController : ControllerBase
    {
        private readonly NcamaContext _context;

        public NcamaApiController(NcamaContext context)
        {
            _context = context;
        }

        // GET: api/NcamaApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NcamaItem>>> GetAll()
        {
            return await _context.Ncama.ToListAsync();
        }

        // GET: api/NcamaApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NcamaItem>> GetById(int id)
        {
            var item = await _context.Ncama.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        // POST: api/NcamaApi
        [HttpPost]
        public async Task<ActionResult<NcamaItem>> Create(NcamaItem item)
        {
            _context.Ncama.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = item.ID_Ncama }, item);
        }

        // PUT: api/NcamaApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, NcamaItem item)
        {
            if (id != item.ID_Ncama)
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
                if (!NcamaItemExists(id))
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

        // DELETE: api/NcamaApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Ncama.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Ncama.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NcamaItemExists(int id)
        {
            return _context.Ncama.Any(e => e.ID_Ncama == id);
        }
    }
}
