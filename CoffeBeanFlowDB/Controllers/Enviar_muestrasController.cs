using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeBeanFlowDB.Contexts;
using CoffeBeanFlowDB.Models;

namespace CoffeBeanFlowDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnviarMuestrasApiController : ControllerBase
    {
        private readonly Enviar_muestrasContext _context;

        public EnviarMuestrasApiController(Enviar_muestrasContext context)
        {
            _context = context;
        }

        // GET: api/EnviarMuestrasApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enviar_muestrasItem>>> GetEnviarMuestras()
        {
            return await _context.Enviar_muestras.ToListAsync();
        }

        // GET: api/EnviarMuestrasApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Enviar_muestrasItem>> GetEnviarMuestrasItem(int id)
        {
            var enviarMuestrasItem = await _context.Enviar_muestras.FindAsync(id);

            if (enviarMuestrasItem == null)
            {
                return NotFound();
            }

            return enviarMuestrasItem;
        }

        // POST: api/EnviarMuestrasApi
        [HttpPost]
        public async Task<ActionResult<Enviar_muestrasItem>> PostEnviarMuestrasItem(Enviar_muestrasItem enviarMuestrasItem)
        {
            _context.Enviar_muestras.Add(enviarMuestrasItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEnviarMuestrasItem), new { id = enviarMuestrasItem.ID_Trilla }, enviarMuestrasItem);
        }

        // PUT: api/EnviarMuestrasApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnviarMuestrasItem(int id, Enviar_muestrasItem enviarMuestrasItem)
        {
            if (id != enviarMuestrasItem.ID_Trilla)
            {
                return BadRequest();
            }

            _context.Entry(enviarMuestrasItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnviarMuestrasItemExists(id))
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

        // DELETE: api/EnviarMuestrasApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnviarMuestrasItem(int id)
        {
            var enviarMuestrasItem = await _context.Enviar_muestras.FindAsync(id);
            if (enviarMuestrasItem == null)
            {
                return NotFound();
            }

            _context.Enviar_muestras.Remove(enviarMuestrasItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EnviarMuestrasItemExists(int id)
        {
            return _context.Enviar_muestras.Any(e => e.ID_Trilla == id);
        }
    }
}
