using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeBeanFlowDB.Contexts;
using CoffeBeanFlowDB.Models;


namespace CoffeBeanFlowDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperaturaSecadoApiController : ControllerBase
    {
        private readonly TemperaturaSecadoContext _context;

        public TemperaturaSecadoApiController(TemperaturaSecadoContext context)
        {
            _context = context;
        }

        // GET: api/TemperaturaSecadoApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TemperaturaSecadoItem>>> GetAll()
        {
            return await _context.TemperaturaSecado.ToListAsync();
        }

        // GET: api/TemperaturaSecadoApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TemperaturaSecadoItem>> GetById(int id)
        {
            var item = await _context.TemperaturaSecado.FindAsync(id);
            if (item == null)
                return NotFound();

            return item;
        }

        // POST: api/TemperaturaSecadoApi
        [HttpPost]
        public async Task<ActionResult<TemperaturaSecadoItem>> Create(TemperaturaSecadoItem item)
        {
            _context.TemperaturaSecado.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = item.ID_Temperatura }, item);
        }

        // PUT: api/TemperaturaSecadoApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TemperaturaSecadoItem item)
        {
            if (id != item.ID_Temperatura)
                return BadRequest();

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemperaturaSecadoItemExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/TemperaturaSecadoApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.TemperaturaSecado.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.TemperaturaSecado.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TemperaturaSecadoItemExists(int id)
        {
            return _context.TemperaturaSecado.Any(e => e.ID_Temperatura == id);
        }
    }
}
