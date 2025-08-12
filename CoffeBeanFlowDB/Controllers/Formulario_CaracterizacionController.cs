using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeBeanFlowDB.Contexts;
using CoffeBeanFlowDB.Models;

namespace CoffeBeanFlowDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormularioCaracterizacionApiController : ControllerBase
    {
        private readonly Formulario_CaracterizacionContext _context;

        public FormularioCaracterizacionApiController(Formulario_CaracterizacionContext context)
        {
            _context = context;
        }

        // GET: api/FormularioCaracterizacionApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Formulario_CaracterizacionItem>>> GetFormularioCaracterizacion()
        {
            return await _context.Formulario_Caracterizacion.ToListAsync();
        }

        // GET: api/FormularioCaracterizacionApi/2025-06-24T10:00:00
        [HttpGet("{id}")]
        public async Task<ActionResult<Formulario_CaracterizacionItem>> GetFormularioCaracterizacionItem(DateTime id)
        {
            var item = await _context.Formulario_Caracterizacion.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // POST: api/FormularioCaracterizacionApi
        [HttpPost]
        public async Task<ActionResult<Formulario_CaracterizacionItem>> PostFormularioCaracterizacionItem(Formulario_CaracterizacionItem item)
        {
            _context.Formulario_Caracterizacion.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFormularioCaracterizacionItem), new { id = item.Tiempo }, item);
        }

        // PUT: api/FormularioCaracterizacionApi/2025-06-24T10:00:00
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFormularioCaracterizacionItem(DateTime id, Formulario_CaracterizacionItem item)
        {
            if (id != item.Tiempo)
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
                if (!FormularioCaracterizacionItemExists(id))
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

        // DELETE: api/FormularioCaracterizacionApi/2025-06-24T10:00:00
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormularioCaracterizacionItem(DateTime id)
        {
            var item = await _context.Formulario_Caracterizacion.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Formulario_Caracterizacion.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FormularioCaracterizacionItemExists(DateTime id)
        {
            return _context.Formulario_Caracterizacion.Any(e => e.Tiempo == id);
        }
    }
}
