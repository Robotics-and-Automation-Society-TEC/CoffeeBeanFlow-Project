using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeeBeanFlowAPI.Data;
using CoffeeBeanFlowAPI.Models;

namespace CoffeeBeanFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecadoController : ControllerBase
    {
        private readonly CoffeeBeanFlowDbContext _context;

        public SecadoController(CoffeeBeanFlowDbContext context)
        {
            _context = context;
        }

        // GET: api/Secado
        /// <summary>
        /// Obtiene todos los registros de secado con sus entidades débiles
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SecadoEntity>>> GetSecado()
        {
            return await _context.Secado
                .Include(s => s.Humedades)
                .Include(s => s.TermoHigrometrias)
                .Include(s => s.TemperaturasSecado)
                .Include(s => s.Ncamas)
                .Include(s => s.AreaAcopio)
                .ToListAsync();
        }

        // GET: api/Secado/5
        /// <summary>
        /// Obtiene un registro de secado específico por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<SecadoEntity>> GetSecado(int id)
        {
            var secado = await _context.Secado
                .Include(s => s.Humedades)
                .Include(s => s.TermoHigrometrias)
                .Include(s => s.TemperaturasSecado)
                .Include(s => s.Ncamas)
                .Include(s => s.AreaAcopio)
                .FirstOrDefaultAsync(s => s.IdSecado == id);

            if (secado == null)
            {
                return NotFound($"No se encontró el registro de secado con ID {id}");
            }

            return secado;
        }

        // GET: api/Secado/ByLote/LOTE-001
        /// <summary>
        /// Obtiene todos los registros de secado asociados a un lote específico
        /// </summary>
        [HttpGet("ByLote/{nlote}")]
        public async Task<ActionResult<IEnumerable<SecadoEntity>>> GetSecadoByLote(string nlote)
        {
            var secados = await _context.Secado
                .Include(s => s.Humedades)
                .Include(s => s.TermoHigrometrias)
                .Include(s => s.TemperaturasSecado)
                .Include(s => s.Ncamas)
                .Where(s => s.Nlote == nlote)
                .ToListAsync();

            if (!secados.Any())
            {
                return NotFound($"No se encontraron registros de secado para el lote {nlote}");
            }

            return secados;
        }

        // POST: api/Secado
        /// <summary>
        /// Crea un nuevo registro de secado con sus entidades débiles
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<SecadoEntity>> PostSecado(SecadoEntity secado)
        {
            // Verificar que el lote existe
            var loteExists = await _context.AreaAcopio.AnyAsync(a => a.Nlote == secado.Nlote);
            if (!loteExists)
            {
                return BadRequest($"El lote {secado.Nlote} no existe en el área de acopio");
            }

            _context.Secado.Add(secado);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Conflict("Error al guardar el registro de secado");
            }

            return CreatedAtAction(nameof(GetSecado), new { id = secado.IdSecado }, secado);
        }

        // PUT: api/Secado/5
        /// <summary>
        /// Actualiza un registro de secado existente y sus entidades débiles
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSecado(int id, SecadoEntity secado)
        {
            if (id != secado.IdSecado)
            {
                return BadRequest("El ID no coincide");
            }

            // Verificar que el lote existe
            var loteExists = await _context.AreaAcopio.AnyAsync(a => a.Nlote == secado.Nlote);
            if (!loteExists)
            {
                return BadRequest($"El lote {secado.Nlote} no existe en el área de acopio");
            }

            // Obtener el registro existente con sus entidades débiles
            var secadoExistente = await _context.Secado
                .Include(s => s.Humedades)
                .Include(s => s.TermoHigrometrias)
                .Include(s => s.TemperaturasSecado)
                .Include(s => s.Ncamas)
                .FirstOrDefaultAsync(s => s.IdSecado == id);

            if (secadoExistente == null)
            {
                return NotFound($"No se encontró el registro de secado con ID {id}");
            }

            // Actualizar propiedades principales
            secadoExistente.Nlote = secado.Nlote;
            secadoExistente.Finicio = secado.Finicio;
            secadoExistente.Dsecado = secado.Dsecado;
            secadoExistente.Ffinal = secado.Ffinal;

            // Actualizar entidades débiles: Humedad
            _context.Humedad.RemoveRange(secadoExistente.Humedades);
            secadoExistente.Humedades = secado.Humedades;

            // Actualizar entidades débiles: TermoHigrometria
            _context.TermoHigrometria.RemoveRange(secadoExistente.TermoHigrometrias);
            secadoExistente.TermoHigrometrias = secado.TermoHigrometrias;

            // Actualizar entidades débiles: TemperaturaSecado
            _context.TemperaturaSecado.RemoveRange(secadoExistente.TemperaturasSecado);
            secadoExistente.TemperaturasSecado = secado.TemperaturasSecado;

            // Actualizar entidades débiles: Ncama
            _context.Ncama.RemoveRange(secadoExistente.Ncamas);
            secadoExistente.Ncamas = secado.Ncamas;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SecadoExists(id))
                {
                    return NotFound($"No se encontró el registro de secado con ID {id}");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Secado/5
        /// <summary>
        /// Elimina un registro de secado y sus entidades débiles (cascade)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSecado(int id)
        {
            var secado = await _context.Secado.FindAsync(id);
            if (secado == null)
            {
                return NotFound($"No se encontró el registro de secado con ID {id}");
            }

            _context.Secado.Remove(secado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SecadoExists(int id)
        {
            return _context.Secado.Any(e => e.IdSecado == id);
        }
    }
}
