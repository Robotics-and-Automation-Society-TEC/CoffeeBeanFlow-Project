using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeeBeanFlowAPI.Data;
using CoffeeBeanFlowAPI.Models;

namespace CoffeeBeanFlowAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrillaController : ControllerBase
    {
        private readonly CoffeeBeanFlowDbContext _context;
        private readonly ILogger<TrillaController> _logger;

        public TrillaController(CoffeeBeanFlowDbContext context, ILogger<TrillaController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Trilla
        /// <summary>
        /// Obtiene todos los registros de trilla con PesoVerde
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrillaEntity>>> GetTrilla()
        {
            try
            {
                var trillas = await _context.Trilla
                    .Include(t => t.AreaAcopio)
                    .Include(t => t.PesoVerde)
                    .ToListAsync();

                return Ok(trillas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los registros de trilla");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // GET: api/Trilla/5
        /// <summary>
        /// Obtiene un registro de trilla por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<TrillaEntity>> GetTrilla(int id)
        {
            try
            {
                var trilla = await _context.Trilla
                    .Include(t => t.AreaAcopio)
                    .Include(t => t.PesoVerde)
                    .FirstOrDefaultAsync(t => t.IdTrilla == id);

                if (trilla == null)
                {
                    return NotFound($"No se encontró trilla con ID {id}");
                }

                return Ok(trilla);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener trilla con ID {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // GET: api/Trilla/lote/{nlote}
        /// <summary>
        /// Obtiene todos los registros de trilla por número de lote
        /// </summary>
        [HttpGet("lote/{nlote}")]
        public async Task<ActionResult<IEnumerable<TrillaEntity>>> GetTrillasPorLote(string nlote)
        {
            try
            {
                var trillas = await _context.Trilla
                    .Include(t => t.AreaAcopio)
                    .Include(t => t.PesoVerde)
                    .Where(t => t.Nlote == nlote)
                    .ToListAsync();

                if (!trillas.Any())
                {
                    return NotFound($"No se encontraron registros de trilla para el lote {nlote}");
                }

                return Ok(trillas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener trillas del lote {Nlote}", nlote);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // POST: api/Trilla
        /// <summary>
        /// Crea un nuevo registro de trilla con PesoVerde opcional
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<TrillaEntity>> PostTrilla(TrillaEntity trilla)
        {
            try
            {
                // Validar que el Nlote existe en AreaAcopio
                var areaAcopioExists = await _context.AreaAcopio.AnyAsync(a => a.Nlote == trilla.Nlote);
                if (!areaAcopioExists)
                {
                    return BadRequest($"El número de lote '{trilla.Nlote}' no existe en Área de Acopio");
                }

                _context.Trilla.Add(trilla);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTrilla), new { id = trilla.IdTrilla }, trilla);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear trilla");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // PUT: api/Trilla/5
        /// <summary>
        /// Actualiza un registro de trilla existente con PesoVerde
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrilla(int id, TrillaEntity trilla)
        {
            if (id != trilla.IdTrilla)
            {
                return BadRequest("El ID del registro no coincide");
            }

            try
            {
                var trillaExistente = await _context.Trilla
                    .Include(t => t.PesoVerde)
                    .FirstOrDefaultAsync(t => t.IdTrilla == id);

                if (trillaExistente == null)
                {
                    return NotFound($"No se encontró trilla con ID {id}");
                }

                // Validar que el Nlote existe
                var areaAcopioExists = await _context.AreaAcopio.AnyAsync(a => a.Nlote == trilla.Nlote);
                if (!areaAcopioExists)
                {
                    return BadRequest($"El número de lote '{trilla.Nlote}' no existe en Área de Acopio");
                }

                // Actualizar propiedades simples de Trilla
                trillaExistente.Nlote = trilla.Nlote;
                trillaExistente.Hinicial = trilla.Hinicial;
                trillaExistente.Hfinal = trilla.Hfinal;
                trillaExistente.RFinalPelado = trilla.RFinalPelado;
                trillaExistente.RFinalSeleccion = trilla.RFinalSeleccion;
                trillaExistente.WverdeFinal = trilla.WverdeFinal;
                trillaExistente.RteoricoPelado = trilla.RteoricoPelado;
                trillaExistente.WverdeTeorico = trilla.WverdeTeorico;
                trillaExistente.RTeoricoSeleccion = trilla.RTeoricoSeleccion;
                trillaExistente.FfinalReposo = trilla.FfinalReposo;
                trillaExistente.Psegundas = trilla.Psegundas;
                trillaExistente.Pcataduras = trilla.Pcataduras;
                trillaExistente.Pbarreduras = trilla.Pbarreduras;
                trillaExistente.Pescogeduras = trilla.Pescogeduras;
                trillaExistente.Pcaracolillo = trilla.Pcaracolillo;
                trillaExistente.Pprimera = trilla.Pprimera;
                trillaExistente.Pmadres = trilla.Pmadres;
                trillaExistente.Pmenudos = trilla.Pmenudos;
                trillaExistente.Pinferiores = trilla.Pinferiores;

                // Actualizar o crear PesoVerde (relación 1:1)
                if (trilla.PesoVerde != null)
                {
                    if (trillaExistente.PesoVerde == null)
                    {
                        // Crear nuevo PesoVerde
                        trilla.PesoVerde.IdPesoVerde = id; // FK debe ser igual al ID de Trilla
                        _context.PesoVerde.Add(trilla.PesoVerde);
                    }
                    else
                    {
                        // Actualizar PesoVerde existente
                        trillaExistente.PesoVerde.Winferiores = trilla.PesoVerde.Winferiores;
                        trillaExistente.PesoVerde.Wfinal = trilla.PesoVerde.Wfinal;
                        trillaExistente.PesoVerde.WFinalInferiores = trilla.PesoVerde.WFinalInferiores;
                    }
                }
                else if (trillaExistente.PesoVerde != null)
                {
                    // Eliminar PesoVerde si se envió null
                    _context.PesoVerde.Remove(trillaExistente.PesoVerde);
                }

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrillaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar trilla con ID {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // DELETE: api/Trilla/5
        /// <summary>
        /// Elimina un registro de trilla por ID (cascade elimina PesoVerde)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrilla(int id)
        {
            try
            {
                var trilla = await _context.Trilla.FindAsync(id);
                if (trilla == null)
                {
                    return NotFound($"No se encontró trilla con ID {id}");
                }

                _context.Trilla.Remove(trilla);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar trilla con ID {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        private bool TrillaExists(int id)
        {
            return _context.Trilla.Any(e => e.IdTrilla == id);
        }
    }
}
