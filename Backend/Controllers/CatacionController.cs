using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeeBeanFlowAPI.Data;
using Backend.Models;

namespace CoffeeBeanFlowAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatacionController : ControllerBase
    {
        private readonly CoffeeBeanFlowDbContext _context;

        public CatacionController(CoffeeBeanFlowDbContext context)
        {
            _context = context;
        }

        // GET: api/Catacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatacionEntity>>> GetCataciones()
        {
            return await _context.Catacion
                .Include(c => c.Rondas)
                .ToListAsync();
        }

        // GET: api/Catacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CatacionEntity>> GetCatacion(int id)
        {
            var catacion = await _context.Catacion
                .Include(c => c.Rondas)
                .FirstOrDefaultAsync(c => c.IdCatacion == id);

            if (catacion == null)
            {
                return NotFound(new { message = $"Catación con ID {id} no encontrada" });
            }

            return catacion;
        }

        // GET: api/Catacion/lote/{nlote}
        [HttpGet("lote/{nlote}")]
        public async Task<ActionResult<IEnumerable<CatacionEntity>>> GetCatacionesPorLote(string nlote)
        {
            var cataciones = await _context.Catacion
                .Include(c => c.Rondas)
                .Where(c => c.Nlote == nlote)
                .ToListAsync();

            return cataciones;
        }

        // POST: api/Catacion
        [HttpPost]
        public async Task<ActionResult<CatacionEntity>> CreateCatacion([FromBody] CatacionEntity catacion)
        {
            // Agregar la catación
            _context.Catacion.Add(catacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCatacion), new { id = catacion.IdCatacion }, catacion);
        }

        // PUT: api/Catacion/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCatacion(int id, [FromBody] CatacionEntity catacion)
        {
            if (id != catacion.IdCatacion)
            {
                return BadRequest(new { message = "El ID de la catación no coincide" });
            }

            var catacionExistente = await _context.Catacion
                .Include(c => c.Rondas)
                .FirstOrDefaultAsync(c => c.IdCatacion == id);

            if (catacionExistente == null)
            {
                return NotFound(new { message = $"Catación con ID {id} no encontrada" });
            }

            // Actualizar propiedades de la catación
            _context.Entry(catacionExistente).CurrentValues.SetValues(catacion);

            // Actualizar Rondas
            // Eliminar rondas que ya no están en la lista
            var rondasAEliminar = catacionExistente.Rondas
                .Where(re => !catacion.Rondas.Any(r => r.IdRondas == re.IdRondas))
                .ToList();
            _context.Rondas.RemoveRange(rondasAEliminar);

            // Agregar o actualizar rondas
            foreach (var ronda in catacion.Rondas)
            {
                var rondaExistente = catacionExistente.Rondas
                    .FirstOrDefault(r => r.IdRondas == ronda.IdRondas);

                if (rondaExistente != null)
                {
                    // Actualizar
                    _context.Entry(rondaExistente).CurrentValues.SetValues(ronda);
                }
                else
                {
                    // Agregar nueva
                    ronda.IdCatacion = id;
                    catacionExistente.Rondas.Add(ronda);
                }
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Catacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatacion(int id)
        {
            var catacion = await _context.Catacion
                .Include(c => c.Rondas)
                .FirstOrDefaultAsync(c => c.IdCatacion == id);

            if (catacion == null)
            {
                return NotFound(new { message = $"Catación con ID {id} no encontrada" });
            }

            _context.Catacion.Remove(catacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Catacion/5/rondas
        [HttpPost("{id}/rondas")]
        public async Task<ActionResult<RondasEntity>> AgregarRonda(int id, [FromBody] RondasEntity ronda)
        {
            var catacion = await _context.Catacion.FindAsync(id);

            if (catacion == null)
            {
                return NotFound(new { message = $"Catación con ID {id} no encontrada" });
            }

            ronda.IdCatacion = id;
            _context.Rondas.Add(ronda);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRonda), new { id = id, idRonda = ronda.IdRondas }, ronda);
        }

        // GET: api/Catacion/5/rondas/10
        [HttpGet("{id}/rondas/{idRonda}")]
        public async Task<ActionResult<RondasEntity>> GetRonda(int id, int idRonda)
        {
            var ronda = await _context.Rondas
                .FirstOrDefaultAsync(r => r.IdCatacion == id && r.IdRondas == idRonda);

            if (ronda == null)
            {
                return NotFound(new { message = $"Ronda con ID {idRonda} no encontrada para catación {id}" });
            }

            return ronda;
        }

        // PUT: api/Catacion/5/rondas/10
        [HttpPut("{id}/rondas/{idRonda}")]
        public async Task<IActionResult> ActualizarRonda(int id, int idRonda, [FromBody] RondasEntity ronda)
        {
            if (idRonda != ronda.IdRondas)
            {
                return BadRequest(new { message = "El ID de la ronda no coincide" });
            }

            var rondaExistente = await _context.Rondas
                .FirstOrDefaultAsync(r => r.IdCatacion == id && r.IdRondas == idRonda);

            if (rondaExistente == null)
            {
                return NotFound(new { message = $"Ronda con ID {idRonda} no encontrada" });
            }

            rondaExistente.ValorCalidad = ronda.ValorCalidad;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Catacion/5/rondas/10
        [HttpDelete("{id}/rondas/{idRonda}")]
        public async Task<IActionResult> EliminarRonda(int id, int idRonda)
        {
            var ronda = await _context.Rondas
                .FirstOrDefaultAsync(r => r.IdCatacion == id && r.IdRondas == idRonda);

            if (ronda == null)
            {
                return NotFound(new { message = $"Ronda con ID {idRonda} no encontrada" });
            }

            _context.Rondas.Remove(ronda);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Catacion/5/rondas
        [HttpGet("{id}/rondas")]
        public async Task<ActionResult<IEnumerable<RondasEntity>>> GetRondasPorCatacion(int id)
        {
            var catacion = await _context.Catacion.FindAsync(id);

            if (catacion == null)
            {
                return NotFound(new { message = $"Catación con ID {id} no encontrada" });
            }

            var rondas = await _context.Rondas
                .Where(r => r.IdCatacion == id)
                .ToListAsync();

            return rondas;
        }
    }
}
