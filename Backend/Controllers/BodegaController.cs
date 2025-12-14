using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeeBeanFlowAPI.Data;
using CoffeeBeanFlowAPI.Models;

namespace CoffeeBeanFlowAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BodegaController : ControllerBase
    {
        private readonly CoffeeBeanFlowDbContext _context;
        private readonly ILogger<BodegaController> _logger;

        public BodegaController(CoffeeBeanFlowDbContext context, ILogger<BodegaController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Bodega
        /// <summary>
        /// Obtiene todos los registros de bodega con relaciones N:N
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BodegaEntity>>> GetBodegas()
        {
            try
            {
                var bodegas = await _context.Bodega
                    .Include(b => b.AreaAcopio)
                    .Include(b => b.SecadosGuardados)
                        .ThenInclude(sg => sg.Secado)
                    .ToListAsync();

                return Ok(bodegas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los registros de bodega");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // GET: api/Bodega/5
        /// <summary>
        /// Obtiene un registro de bodega por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<BodegaEntity>> GetBodega(int id)
        {
            try
            {
                var bodega = await _context.Bodega
                    .Include(b => b.AreaAcopio)
                    .Include(b => b.SecadosGuardados)
                        .ThenInclude(sg => sg.Secado)
                    .FirstOrDefaultAsync(b => b.IdBodega == id);

                if (bodega == null)
                {
                    return NotFound($"No se encontró bodega con ID {id}");
                }

                return Ok(bodega);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener bodega con ID {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // GET: api/Bodega/lote/{nlote}
        /// <summary>
        /// Obtiene todos los registros de bodega por número de lote
        /// </summary>
        [HttpGet("lote/{nlote}")]
        public async Task<ActionResult<IEnumerable<BodegaEntity>>> GetBodegasPorLote(string nlote)
        {
            try
            {
                var bodegas = await _context.Bodega
                    .Include(b => b.AreaAcopio)
                    .Include(b => b.SecadosGuardados)
                        .ThenInclude(sg => sg.Secado)
                    .Where(b => b.Nlote == nlote)
                    .ToListAsync();

                if (!bodegas.Any())
                {
                    return NotFound($"No se encontraron registros de bodega para el lote {nlote}");
                }

                return Ok(bodegas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener bodegas del lote {Nlote}", nlote);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // POST: api/Bodega
        /// <summary>
        /// Crea un nuevo registro de bodega con relaciones N:N opcionales
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<BodegaEntity>> PostBodega(BodegaEntity bodega)
        {
            try
            {
                // Validar que el Nlote existe en AreaAcopio
                var areaAcopioExists = await _context.AreaAcopio.AnyAsync(a => a.Nlote == bodega.Nlote);
                if (!areaAcopioExists)
                {
                    return BadRequest($"El número de lote '{bodega.Nlote}' no existe en Área de Acopio");
                }

                // Validar IDs de secado si se proporcionan
                if (bodega.SecadosGuardados != null && bodega.SecadosGuardados.Any())
                {
                    var secadoIds = bodega.SecadosGuardados.Select(sg => sg.IdSecado).ToList();
                    var secadosValidos = await _context.Secado
                        .Where(s => secadoIds.Contains(s.IdSecado))
                        .Select(s => s.IdSecado)
                        .ToListAsync();

                    var idsInvalidos = secadoIds.Except(secadosValidos).ToList();
                    if (idsInvalidos.Any())
                    {
                        return BadRequest($"Los siguientes IDs de Secado no existen: {string.Join(", ", idsInvalidos)}");
                    }
                }

                _context.Bodega.Add(bodega);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetBodega), new { id = bodega.IdBodega }, bodega);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear bodega");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // PUT: api/Bodega/5
        /// <summary>
        /// Actualiza un registro de bodega existente con relaciones N:N
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBodega(int id, BodegaEntity bodega)
        {
            if (id != bodega.IdBodega)
            {
                return BadRequest("El ID del registro no coincide");
            }

            try
            {
                var bodegaExistente = await _context.Bodega
                    .Include(b => b.SecadosGuardados)
                    .FirstOrDefaultAsync(b => b.IdBodega == id);

                if (bodegaExistente == null)
                {
                    return NotFound($"No se encontró bodega con ID {id}");
                }

                // Validar que el Nlote existe
                var areaAcopioExists = await _context.AreaAcopio.AnyAsync(a => a.Nlote == bodega.Nlote);
                if (!areaAcopioExists)
                {
                    return BadRequest($"El número de lote '{bodega.Nlote}' no existe en Área de Acopio");
                }

                // Actualizar propiedades simples
                bodegaExistente.Nlote = bodega.Nlote;
                bodegaExistente.WBellota = bodega.WBellota;
                bodegaExistente.WPergamino = bodega.WPergamino;
                bodegaExistente.Hfinal = bodega.Hfinal;
                bodegaExistente.Hinicial = bodega.Hinicial;
                bodegaExistente.DPergamino = bodega.DPergamino;
                bodegaExistente.DBellota = bodega.DBellota;
                bodegaExistente.FinicioReposo = bodega.FinicioReposo;
                bodegaExistente.CantidadSacos = bodega.CantidadSacos;
                bodegaExistente.PMHRelativa = bodega.PMHRelativa;
                bodegaExistente.PMTInterna = bodega.PMTInterna;
                bodegaExistente.PMTExterna = bodega.PMTExterna;

                // Actualizar relaciones N:N (Guardar_Cafe)
                if (bodega.SecadosGuardados != null)
                {
                    // Validar IDs de secado
                    var secadoIds = bodega.SecadosGuardados.Select(sg => sg.IdSecado).ToList();
                    var secadosValidos = await _context.Secado
                        .Where(s => secadoIds.Contains(s.IdSecado))
                        .Select(s => s.IdSecado)
                        .ToListAsync();

                    var idsInvalidos = secadoIds.Except(secadosValidos).ToList();
                    if (idsInvalidos.Any())
                    {
                        return BadRequest($"Los siguientes IDs de Secado no existen: {string.Join(", ", idsInvalidos)}");
                    }

                    // Eliminar relaciones existentes
                    _context.GuardarCafe.RemoveRange(bodegaExistente.SecadosGuardados);

                    // Agregar nuevas relaciones
                    foreach (var guardarCafe in bodega.SecadosGuardados)
                    {
                        guardarCafe.IdBodega = id;
                        bodegaExistente.SecadosGuardados.Add(guardarCafe);
                    }
                }

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BodegaExists(id))
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
                _logger.LogError(ex, "Error al actualizar bodega con ID {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // DELETE: api/Bodega/5
        /// <summary>
        /// Elimina un registro de bodega por ID
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBodega(int id)
        {
            try
            {
                var bodega = await _context.Bodega.FindAsync(id);
                if (bodega == null)
                {
                    return NotFound($"No se encontró bodega con ID {id}");
                }

                _context.Bodega.Remove(bodega);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar bodega con ID {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // POST: api/Bodega/{idBodega}/guardar-secado/{idSecado}
        /// <summary>
        /// Asocia un Secado con una Bodega (relación Guardar_Cafe)
        /// </summary>
        [HttpPost("{idBodega}/guardar-secado/{idSecado}")]
        public async Task<IActionResult> GuardarSecadoEnBodega(int idBodega, int idSecado, [FromBody] int cantidadSacos)
        {
            try
            {
                // Validar que la bodega existe
                var bodegaExists = await _context.Bodega.AnyAsync(b => b.IdBodega == idBodega);
                if (!bodegaExists)
                {
                    return NotFound($"No se encontró bodega con ID {idBodega}");
                }

                // Validar que el secado existe
                var secadoExists = await _context.Secado.AnyAsync(s => s.IdSecado == idSecado);
                if (!secadoExists)
                {
                    return NotFound($"No se encontró secado con ID {idSecado}");
                }

                // Verificar si ya existe la relación
                var relacionExiste = await _context.GuardarCafe
                    .AnyAsync(gc => gc.IdBodega == idBodega && gc.IdSecado == idSecado);

                if (relacionExiste)
                {
                    return BadRequest("Ya existe una relación entre este Secado y esta Bodega");
                }

                // Crear la relación
                var guardarCafe = new GuardarCafeEntity
                {
                    IdBodega = idBodega,
                    IdSecado = idSecado,
                    CantidadSacos = cantidadSacos
                };

                _context.GuardarCafe.Add(guardarCafe);
                await _context.SaveChangesAsync();

                return Ok(guardarCafe);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar secado {IdSecado} en bodega {IdBodega}", idSecado, idBodega);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // DELETE: api/Bodega/{idBodega}/guardar-secado/{idSecado}
        /// <summary>
        /// Elimina la asociación entre un Secado y una Bodega
        /// </summary>
        [HttpDelete("{idBodega}/guardar-secado/{idSecado}")]
        public async Task<IActionResult> EliminarSecadoDeBodega(int idBodega, int idSecado)
        {
            try
            {
                var guardarCafe = await _context.GuardarCafe
                    .FirstOrDefaultAsync(gc => gc.IdBodega == idBodega && gc.IdSecado == idSecado);

                if (guardarCafe == null)
                {
                    return NotFound("No existe una relación entre este Secado y esta Bodega");
                }

                _context.GuardarCafe.Remove(guardarCafe);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar secado {IdSecado} de bodega {IdBodega}", idSecado, idBodega);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        private bool BodegaExists(int id)
        {
            return _context.Bodega.Any(e => e.IdBodega == id);
        }
    }
}
