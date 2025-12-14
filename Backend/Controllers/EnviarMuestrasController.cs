using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeeBeanFlowAPI.Data;
using Backend.Models;

namespace CoffeeBeanFlowAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnviarMuestrasController : ControllerBase
    {
        private readonly CoffeeBeanFlowDbContext _context;

        public EnviarMuestrasController(CoffeeBeanFlowDbContext context)
        {
            _context = context;
        }

        // GET: api/EnviarMuestras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnviarMuestrasEntity>>> GetTodasRelaciones()
        {
            return await _context.EnviarMuestras
                .Include(em => em.Trilla)
                .Include(em => em.Catacion)
                .ToListAsync();
        }

        // GET: api/EnviarMuestras/trilla/5
        [HttpGet("trilla/{idTrilla}")]
        public async Task<ActionResult<IEnumerable<EnviarMuestrasEntity>>> GetPorTrilla(int idTrilla)
        {
            var relaciones = await _context.EnviarMuestras
                .Include(em => em.Trilla)
                .Include(em => em.Catacion)
                .Where(em => em.IdTrilla == idTrilla)
                .ToListAsync();

            return relaciones;
        }

        // GET: api/EnviarMuestras/catacion/5
        [HttpGet("catacion/{idCatacion}")]
        public async Task<ActionResult<IEnumerable<EnviarMuestrasEntity>>> GetPorCatacion(int idCatacion)
        {
            var relaciones = await _context.EnviarMuestras
                .Include(em => em.Trilla)
                .Include(em => em.Catacion)
                .Where(em => em.IdCatacion == idCatacion)
                .ToListAsync();

            return relaciones;
        }

        // GET: api/EnviarMuestras/5/10
        [HttpGet("{idTrilla}/{idCatacion}")]
        public async Task<ActionResult<EnviarMuestrasEntity>> GetRelacion(int idTrilla, int idCatacion)
        {
            var relacion = await _context.EnviarMuestras
                .Include(em => em.Trilla)
                .Include(em => em.Catacion)
                .FirstOrDefaultAsync(em => em.IdTrilla == idTrilla && em.IdCatacion == idCatacion);

            if (relacion == null)
            {
                return NotFound(new { message = $"No se encontró relación entre Trilla {idTrilla} y Catación {idCatacion}" });
            }

            return relacion;
        }

        // POST: api/EnviarMuestras
        [HttpPost]
        public async Task<ActionResult<EnviarMuestrasEntity>> CrearRelacion([FromBody] EnviarMuestrasEntity enviarMuestra)
        {
            // Validar que existan Trilla y Catación
            var trillaExists = await _context.Trilla.AnyAsync(t => t.IdTrilla == enviarMuestra.IdTrilla);
            if (!trillaExists)
            {
                return BadRequest(new { message = $"No existe Trilla con ID {enviarMuestra.IdTrilla}" });
            }

            var catacionExists = await _context.Catacion.AnyAsync(c => c.IdCatacion == enviarMuestra.IdCatacion);
            if (!catacionExists)
            {
                return BadRequest(new { message = $"No existe Catación con ID {enviarMuestra.IdCatacion}" });
            }

            // Verificar que no exista ya la relación
            var relacionExiste = await _context.EnviarMuestras
                .AnyAsync(em => em.IdTrilla == enviarMuestra.IdTrilla && em.IdCatacion == enviarMuestra.IdCatacion);

            if (relacionExiste)
            {
                return Conflict(new { message = "Ya existe esta relación entre Trilla y Catación" });
            }

            _context.EnviarMuestras.Add(enviarMuestra);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRelacion), 
                new { idTrilla = enviarMuestra.IdTrilla, idCatacion = enviarMuestra.IdCatacion }, 
                enviarMuestra);
        }

        // PUT: api/EnviarMuestras/5/10
        [HttpPut("{idTrilla}/{idCatacion}")]
        public async Task<IActionResult> ActualizarRelacion(int idTrilla, int idCatacion, [FromBody] EnviarMuestrasEntity enviarMuestra)
        {
            if (idTrilla != enviarMuestra.IdTrilla || idCatacion != enviarMuestra.IdCatacion)
            {
                return BadRequest(new { message = "Los IDs no coinciden" });
            }

            var relacionExistente = await _context.EnviarMuestras
                .FirstOrDefaultAsync(em => em.IdTrilla == idTrilla && em.IdCatacion == idCatacion);

            if (relacionExistente == null)
            {
                return NotFound(new { message = $"No se encontró relación entre Trilla {idTrilla} y Catación {idCatacion}" });
            }

            relacionExistente.FfinalReposo = enviarMuestra.FfinalReposo;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/EnviarMuestras/5/10
        [HttpDelete("{idTrilla}/{idCatacion}")]
        public async Task<IActionResult> EliminarRelacion(int idTrilla, int idCatacion)
        {
            var relacion = await _context.EnviarMuestras
                .FirstOrDefaultAsync(em => em.IdTrilla == idTrilla && em.IdCatacion == idCatacion);

            if (relacion == null)
            {
                return NotFound(new { message = $"No se encontró relación entre Trilla {idTrilla} y Catación {idCatacion}" });
            }

            _context.EnviarMuestras.Remove(relacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
