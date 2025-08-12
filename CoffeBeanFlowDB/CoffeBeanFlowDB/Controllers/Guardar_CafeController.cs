using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeBeanFlowDB.Contexts;
using CoffeBeanFlowDB.Models;

namespace CoffeBeanFlowDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuardarCafeApiController : ControllerBase
    {
        private readonly Guardar_CafeContext _context;

        public GuardarCafeApiController(Guardar_CafeContext context)
        {
            _context = context;
        }

        // GET: api/GuardarCafeApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Guardar_CafeItem>>> GetAll()
        {
            return await _context.Guardar_Cafe.ToListAsync();
        }

        // GET: api/GuardarCafeApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Guardar_CafeItem>> GetById(int id)
        {
            var item = await _context.Guardar_Cafe.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // POST: api/GuardarCafeApi
        [HttpPost]
        public async Task<ActionResult<Guardar_CafeItem>> Create(Guardar_CafeItem item)
        {
            _context.Guardar_Cafe.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = item.ID_Secado }, item);
        }

        // PUT: api/GuardarCafeApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Guardar_CafeItem item)
        {
            if (id != item.ID_Secado)
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
                if (!GuardarCafeItemExists(id))
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

        // DELETE: api/GuardarCafeApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Guardar_Cafe.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Guardar_Cafe.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GuardarCafeItemExists(int id)
        {
            return _context.Guardar_Cafe.Any(e => e.ID_Secado == id);
        }
    }
}
