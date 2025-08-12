using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeBeanFlowDB.Contexts;
using CoffeBeanFlowDB.Models;


namespace CoffeBeanFlowDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecadoApiController : ControllerBase
    {
        private readonly SecadoContext _context;

        public SecadoApiController(SecadoContext context)
        {
            _context = context;
        }

        // GET: api/SecadoApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SecadoItem>>> GetAll()
        {
            return await _context.Secado.ToListAsync();
        }

        // GET: api/SecadoApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SecadoItem>> GetById(int id)
        {
            var item = await _context.Secado.FindAsync(id);
            if (item == null)
                return NotFound();
            return item;
        }

        // POST: api/SecadoApi
        [HttpPost]
        public async Task<ActionResult<SecadoItem>> Create(SecadoItem item)
        {
            _context.Secado.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = item.ID_Secado }, item);
        }

        // PUT: api/SecadoApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SecadoItem item)
        {
            if (id != item.ID_Secado)
                return BadRequest();

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SecadoItemExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/SecadoApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Secado.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.Secado.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SecadoItemExists(int id)
        {
            return _context.Secado.Any(e => e.ID_Secado == id);
        }
    }
}
