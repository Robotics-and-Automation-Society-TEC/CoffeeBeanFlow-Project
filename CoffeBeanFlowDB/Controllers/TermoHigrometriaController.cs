using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeBeanFlowDB.Contexts;
using CoffeBeanFlowDB.Models;


namespace CoffeBeanFlowDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TermoHigrometriaApiController : ControllerBase
    {
        private readonly TermoHigrometriaContext _context;

        public TermoHigrometriaApiController(TermoHigrometriaContext context)
        {
            _context = context;
        }

        // GET: api/TermoHigrometriaApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TermoHigrometriaItem>>> GetAll()
        {
            return await _context.TermoHigrometria.ToListAsync();
        }

        // GET: api/TermoHigrometriaApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TermoHigrometriaItem>> GetById(int id)
        {
            var item = await _context.TermoHigrometria.FindAsync(id);
            if (item == null)
                return NotFound();

            return item;
        }

        // POST: api/TermoHigrometriaApi
        [HttpPost]
        public async Task<ActionResult<TermoHigrometriaItem>> Create(TermoHigrometriaItem item)
        {
            _context.TermoHigrometria.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = item.ID_Termo }, item);
        }

        // PUT: api/TermoHigrometriaApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TermoHigrometriaItem item)
        {
            if (id != item.ID_Termo)
                return BadRequest();

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TermoHigrometriaItemExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/TermoHigrometriaApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.TermoHigrometria.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.TermoHigrometria.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TermoHigrometriaItemExists(int id)
        {
            return _context.TermoHigrometria.Any(e => e.ID_Termo == id);
        }
    }
}
