using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeeBeanFlowAPI.Data;
using Backend.Models;

namespace CoffeeBeanFlowAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuministraController : ControllerBase
    {
        private readonly CoffeeBeanFlowDbContext _context;

        public SuministraController(CoffeeBeanFlowDbContext context)
        {
            _context = context;
        }

        // GET: api/Suministra
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SuministraEntity>>> GetTodasRelaciones()
        {
            return await _context.Suministra
                .Include(s => s.Bodega)
                .Include(s => s.Trilla)
                .ToListAsync();
        }

        // GET: api/Suministra/bodega/5
        [HttpGet("bodega/{idBodega}")]
        public async Task<ActionResult<IEnumerable<SuministraEntity>>> GetPorBodega(int idBodega)
        {
            var relaciones = await _context.Suministra
                .Include(s => s.Bodega)
                .Include(s => s.Trilla)
                .Where(s => s.IdBodega == idBodega)
                .ToListAsync();

            return relaciones;
        }

        // GET: api/Suministra/trilla/5
        [HttpGet("trilla/{idTrilla}")]
        public async Task<ActionResult<IEnumerable<SuministraEntity>>> GetPorTrilla(int idTrilla)
        {
            var relaciones = await _context.Suministra
                .Include(s => s.Bodega)
                .Include(s => s.Trilla)
                .Where(s => s.IdTrilla == idTrilla)
                .ToListAsync();

            return relaciones;
        }

        // GET: api/Suministra/5/10
        [HttpGet("{idBodega}/{idTrilla}")]
        public async Task<ActionResult<SuministraEntity>> GetRelacion(int idBodega, int idTrilla)
        {
            var relacion = await _context.Suministra
                .Include(s => s.Bodega)
                .Include(s => s.Trilla)
                .FirstOrDefaultAsync(s => s.IdBodega == idBodega && s.IdTrilla == idTrilla);

            if (relacion == null)
            {
                return NotFound(new { message = $"No se encontró relación entre Bodega {idBodega} y Trilla {idTrilla}" });
            }

            return relacion;
        }

        // POST: api/Suministra
        [HttpPost]
        public async Task<ActionResult<SuministraEntity>> CrearRelacion([FromBody] SuministraEntity suministra)
        {
            // Validar que existan Bodega y Trilla
            var bodegaExists = await _context.Bodega.AnyAsync(b => b.IdBodega == suministra.IdBodega);
            if (!bodegaExists)
            {
                return BadRequest(new { message = $"No existe Bodega con ID {suministra.IdBodega}" });
            }

            var trillaExists = await _context.Trilla.AnyAsync(t => t.IdTrilla == suministra.IdTrilla);
            if (!trillaExists)
            {
                return BadRequest(new { message = $"No existe Trilla con ID {suministra.IdTrilla}" });
            }

            // Verificar que no exista ya la relación
            var relacionExiste = await _context.Suministra
                .AnyAsync(s => s.IdBodega == suministra.IdBodega && s.IdTrilla == suministra.IdTrilla);

            if (relacionExiste)
            {
                return Conflict(new { message = "Ya existe esta relación entre Bodega y Trilla" });
            }

            _context.Suministra.Add(suministra);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRelacion), 
                new { idBodega = suministra.IdBodega, idTrilla = suministra.IdTrilla }, 
                suministra);
        }

        // DELETE: api/Suministra/5/10
        [HttpDelete("{idBodega}/{idTrilla}")]
        public async Task<IActionResult> EliminarRelacion(int idBodega, int idTrilla)
        {
            var relacion = await _context.Suministra
                .FirstOrDefaultAsync(s => s.IdBodega == idBodega && s.IdTrilla == idTrilla);

            if (relacion == null)
            {
                return NotFound(new { message = $"No se encontró relación entre Bodega {idBodega} y Trilla {idTrilla}" });
            }

            _context.Suministra.Remove(relacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
