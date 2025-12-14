using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeeBeanFlowAPI.Data;
using Backend.Models;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaracterizacionController : ControllerBase
    {
        private readonly CoffeeBeanFlowDbContext _context;

        public CaracterizacionController(CoffeeBeanFlowDbContext context)
        {
            _context = context;
        }

        // GET: api/Caracterizacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaracterizacionEntity>>> GetAll()
        {
            return await _context.Caracterizacion
                .Include(c => c.AreaAcopio)
                .Include(c => c.RCsobremaduras)
                .Include(c => c.RCinmaduras)
                .Include(c => c.RCmaduras)
                .ToListAsync();
        }

        // GET: api/Caracterizacion/{tiempo}
        [HttpGet("{tiempo}")]
        public async Task<ActionResult<CaracterizacionEntity>> GetByTiempo(DateTime tiempo)
        {
            var caracterizacion = await _context.Caracterizacion
                .Include(c => c.AreaAcopio)
                .Include(c => c.RCsobremaduras)
                .Include(c => c.RCinmaduras)
                .Include(c => c.RCmaduras)
                .FirstOrDefaultAsync(c => c.Tiempo == tiempo);

            if (caracterizacion == null)
            {
                return NotFound($"No se encontró caracterización con tiempo: {tiempo}");
            }

            return caracterizacion;
        }

        // GET: api/Caracterizacion/lote/{nlote}
        [HttpGet("lote/{nlote}")]
        public async Task<ActionResult<IEnumerable<CaracterizacionEntity>>> GetByLote(string nlote)
        {
            var caracterizaciones = await _context.Caracterizacion
                .Include(c => c.AreaAcopio)
                .Include(c => c.RCsobremaduras)
                .Include(c => c.RCinmaduras)
                .Include(c => c.RCmaduras)
                .Where(c => c.NloteAreaAcopio == nlote)
                .ToListAsync();

            if (!caracterizaciones.Any())
            {
                return NotFound($"No se encontraron caracterizaciones para el lote: {nlote}");
            }

            return caracterizaciones;
        }

        // POST: api/Caracterizacion
        [HttpPost]
        public async Task<ActionResult<CaracterizacionEntity>> Create([FromBody] CaracterizacionEntity caracterizacion)
        {
            // Validar que el lote existe
            var loteExists = await _context.AreaAcopio.AnyAsync(a => a.Nlote == caracterizacion.NloteAreaAcopio);
            if (!loteExists)
            {
                return BadRequest($"El lote {caracterizacion.NloteAreaAcopio} no existe");
            }

            // Validar que no exista ya una caracterización con el mismo Tiempo
            var exists = await _context.Caracterizacion.AnyAsync(c => c.Tiempo == caracterizacion.Tiempo);
            if (exists)
            {
                return Conflict($"Ya existe una caracterización con el tiempo: {caracterizacion.Tiempo}");
            }

            _context.Caracterizacion.Add(caracterizacion);

            // Agregar entidades débiles si existen
            if (caracterizacion.RCsobremaduras != null)
            {
                caracterizacion.RCsobremaduras.Tiempo = caracterizacion.Tiempo;
                _context.RCsobremaduras.Add(caracterizacion.RCsobremaduras);
            }

            if (caracterizacion.RCinmaduras != null)
            {
                caracterizacion.RCinmaduras.Tiempo = caracterizacion.Tiempo;
                _context.RCinmaduras.Add(caracterizacion.RCinmaduras);
            }

            if (caracterizacion.RCmaduras != null)
            {
                caracterizacion.RCmaduras.Tiempo = caracterizacion.Tiempo;
                _context.RCmaduras.Add(caracterizacion.RCmaduras);
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByTiempo), new { tiempo = caracterizacion.Tiempo }, caracterizacion);
        }

        // PUT: api/Caracterizacion/{tiempo}
        [HttpPut("{tiempo}")]
        public async Task<IActionResult> Update(DateTime tiempo, [FromBody] CaracterizacionEntity caracterizacion)
        {
            if (tiempo != caracterizacion.Tiempo)
            {
                return BadRequest("El tiempo en la URL no coincide con el del cuerpo");
            }

            var caracterizacionExistente = await _context.Caracterizacion
                .Include(c => c.RCsobremaduras)
                .Include(c => c.RCinmaduras)
                .Include(c => c.RCmaduras)
                .FirstOrDefaultAsync(c => c.Tiempo == tiempo);

            if (caracterizacionExistente == null)
            {
                return NotFound($"No se encontró caracterización con tiempo: {tiempo}");
            }

            // Validar que el lote existe
            var loteExists = await _context.AreaAcopio.AnyAsync(a => a.Nlote == caracterizacion.NloteAreaAcopio);
            if (!loteExists)
            {
                return BadRequest($"El lote {caracterizacion.NloteAreaAcopio} no existe");
            }

            // Actualizar campos de caracterización
            caracterizacionExistente.NloteAreaAcopio = caracterizacion.NloteAreaAcopio;
            caracterizacionExistente.DRmaduras = caracterizacion.DRmaduras;
            caracterizacionExistente.PCdebajo = caracterizacion.PCdebajo;
            caracterizacionExistente.Proceso = caracterizacion.Proceso;
            caracterizacionExistente.LAsignado = caracterizacion.LAsignado;
            caracterizacionExistente.Cverdes = caracterizacion.Cverdes;
            caracterizacionExistente.Cobjetivo = caracterizacion.Cobjetivo;
            caracterizacionExistente.Cinmaduras = caracterizacion.Cinmaduras;
            caracterizacionExistente.Csobremaduras = caracterizacion.Csobremaduras;
            caracterizacionExistente.Csecas = caracterizacion.Csecas;
            caracterizacionExistente.Mtabla = caracterizacion.Mtabla;
            caracterizacionExistente.PCverdes = caracterizacion.PCverdes;
            caracterizacionExistente.PCsecas = caracterizacion.PCsecas;
            caracterizacionExistente.PCencima = caracterizacion.PCencima;
            caracterizacionExistente.Emaduracion = caracterizacion.Emaduracion;
            caracterizacionExistente.Broca = caracterizacion.Broca;
            caracterizacionExistente.Densidad = caracterizacion.Densidad;
            caracterizacionExistente.Vanos = caracterizacion.Vanos;
            caracterizacionExistente.Secos = caracterizacion.Secos;
            caracterizacionExistente.PCobjetivo = caracterizacion.PCobjetivo;

            // Actualizar o crear RCsobremaduras
            if (caracterizacion.RCsobremaduras != null)
            {
                if (caracterizacionExistente.RCsobremaduras != null)
                {
                    caracterizacionExistente.RCsobremaduras.Gbx = caracterizacion.RCsobremaduras.Gbx;
                    caracterizacionExistente.RCsobremaduras.Promedio = caracterizacion.RCsobremaduras.Promedio;
                    caracterizacionExistente.RCsobremaduras.Observaciones = caracterizacion.RCsobremaduras.Observaciones;
                }
                else
                {
                    caracterizacion.RCsobremaduras.Tiempo = tiempo;
                    _context.RCsobremaduras.Add(caracterizacion.RCsobremaduras);
                }
            }
            else if (caracterizacionExistente.RCsobremaduras != null)
            {
                _context.RCsobremaduras.Remove(caracterizacionExistente.RCsobremaduras);
            }

            // Actualizar o crear RCinmaduras
            if (caracterizacion.RCinmaduras != null)
            {
                if (caracterizacionExistente.RCinmaduras != null)
                {
                    caracterizacionExistente.RCinmaduras.Gbx = caracterizacion.RCinmaduras.Gbx;
                    caracterizacionExistente.RCinmaduras.Promedio = caracterizacion.RCinmaduras.Promedio;
                    caracterizacionExistente.RCinmaduras.Observaciones = caracterizacion.RCinmaduras.Observaciones;
                }
                else
                {
                    caracterizacion.RCinmaduras.Tiempo = tiempo;
                    _context.RCinmaduras.Add(caracterizacion.RCinmaduras);
                }
            }
            else if (caracterizacionExistente.RCinmaduras != null)
            {
                _context.RCinmaduras.Remove(caracterizacionExistente.RCinmaduras);
            }

            // Actualizar o crear RCmaduras
            if (caracterizacion.RCmaduras != null)
            {
                if (caracterizacionExistente.RCmaduras != null)
                {
                    caracterizacionExistente.RCmaduras.Gbx = caracterizacion.RCmaduras.Gbx;
                    caracterizacionExistente.RCmaduras.Promedio = caracterizacion.RCmaduras.Promedio;
                    caracterizacionExistente.RCmaduras.Observaciones = caracterizacion.RCmaduras.Observaciones;
                }
                else
                {
                    caracterizacion.RCmaduras.Tiempo = tiempo;
                    _context.RCmaduras.Add(caracterizacion.RCmaduras);
                }
            }
            else if (caracterizacionExistente.RCmaduras != null)
            {
                _context.RCmaduras.Remove(caracterizacionExistente.RCmaduras);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Caracterizacion/{tiempo}
        [HttpDelete("{tiempo}")]
        public async Task<IActionResult> Delete(DateTime tiempo)
        {
            var caracterizacion = await _context.Caracterizacion
                .Include(c => c.RCsobremaduras)
                .Include(c => c.RCinmaduras)
                .Include(c => c.RCmaduras)
                .FirstOrDefaultAsync(c => c.Tiempo == tiempo);

            if (caracterizacion == null)
            {
                return NotFound($"No se encontró caracterización con tiempo: {tiempo}");
            }

            // Las entidades débiles se eliminan automáticamente por CASCADE
            _context.Caracterizacion.Remove(caracterizacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Caracterizacion/{tiempo}/sobremaduras
        [HttpPost("{tiempo}/sobremaduras")]
        public async Task<ActionResult<RCsobremadurasEntity>> AddSobremaduras(DateTime tiempo, [FromBody] RCsobremadurasEntity rcSobremaduras)
        {
            var caracterizacion = await _context.Caracterizacion
                .Include(c => c.RCsobremaduras)
                .FirstOrDefaultAsync(c => c.Tiempo == tiempo);

            if (caracterizacion == null)
            {
                return NotFound($"No se encontró caracterización con tiempo: {tiempo}");
            }

            if (caracterizacion.RCsobremaduras != null)
            {
                return Conflict("Esta caracterización ya tiene sobremaduras registradas");
            }

            rcSobremaduras.Tiempo = tiempo;
            _context.RCsobremaduras.Add(rcSobremaduras);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByTiempo), new { tiempo }, rcSobremaduras);
        }

        // DELETE: api/Caracterizacion/{tiempo}/sobremaduras
        [HttpDelete("{tiempo}/sobremaduras")]
        public async Task<IActionResult> DeleteSobremaduras(DateTime tiempo)
        {
            var rcSobremaduras = await _context.RCsobremaduras
                .FirstOrDefaultAsync(rc => rc.Tiempo == tiempo);

            if (rcSobremaduras == null)
            {
                return NotFound($"No se encontraron sobremaduras para el tiempo: {tiempo}");
            }

            _context.RCsobremaduras.Remove(rcSobremaduras);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Caracterizacion/{tiempo}/inmaduras
        [HttpPost("{tiempo}/inmaduras")]
        public async Task<ActionResult<RCinmadurasEntity>> AddInmaduras(DateTime tiempo, [FromBody] RCinmadurasEntity rcInmaduras)
        {
            var caracterizacion = await _context.Caracterizacion
                .Include(c => c.RCinmaduras)
                .FirstOrDefaultAsync(c => c.Tiempo == tiempo);

            if (caracterizacion == null)
            {
                return NotFound($"No se encontró caracterización con tiempo: {tiempo}");
            }

            if (caracterizacion.RCinmaduras != null)
            {
                return Conflict("Esta caracterización ya tiene inmaduras registradas");
            }

            rcInmaduras.Tiempo = tiempo;
            _context.RCinmaduras.Add(rcInmaduras);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByTiempo), new { tiempo }, rcInmaduras);
        }

        // DELETE: api/Caracterizacion/{tiempo}/inmaduras
        [HttpDelete("{tiempo}/inmaduras")]
        public async Task<IActionResult> DeleteInmaduras(DateTime tiempo)
        {
            var rcInmaduras = await _context.RCinmaduras
                .FirstOrDefaultAsync(rc => rc.Tiempo == tiempo);

            if (rcInmaduras == null)
            {
                return NotFound($"No se encontraron inmaduras para el tiempo: {tiempo}");
            }

            _context.RCinmaduras.Remove(rcInmaduras);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Caracterizacion/{tiempo}/maduras
        [HttpPost("{tiempo}/maduras")]
        public async Task<ActionResult<RCmadurasEntity>> AddMaduras(DateTime tiempo, [FromBody] RCmadurasEntity rcMaduras)
        {
            var caracterizacion = await _context.Caracterizacion
                .Include(c => c.RCmaduras)
                .FirstOrDefaultAsync(c => c.Tiempo == tiempo);

            if (caracterizacion == null)
            {
                return NotFound($"No se encontró caracterización con tiempo: {tiempo}");
            }

            if (caracterizacion.RCmaduras != null)
            {
                return Conflict("Esta caracterización ya tiene maduras registradas");
            }

            rcMaduras.Tiempo = tiempo;
            _context.RCmaduras.Add(rcMaduras);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByTiempo), new { tiempo }, rcMaduras);
        }

        // DELETE: api/Caracterizacion/{tiempo}/maduras
        [HttpDelete("{tiempo}/maduras")]
        public async Task<IActionResult> DeleteMaduras(DateTime tiempo)
        {
            var rcMaduras = await _context.RCmaduras
                .FirstOrDefaultAsync(rc => rc.Tiempo == tiempo);

            if (rcMaduras == null)
            {
                return NotFound($"No se encontraron maduras para el tiempo: {tiempo}");
            }

            _context.RCmaduras.Remove(rcMaduras);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
