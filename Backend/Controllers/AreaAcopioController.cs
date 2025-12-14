using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeeBeanFlowAPI.Data;
using CoffeeBeanFlowAPI.Models;

namespace CoffeeBeanFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaAcopioController : ControllerBase
    {
        private readonly CoffeeBeanFlowDbContext _context;

        public AreaAcopioController(CoffeeBeanFlowDbContext context)
        {
            _context = context;
        }

        // GET: api/AreaAcopio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AreaAcopioEntity>>> GetAreaAcopio()
        {
            return await _context.AreaAcopio.ToListAsync();
        }

        // GET: api/AreaAcopio/LOTE-001
        [HttpGet("{nlote}")]
        public async Task<ActionResult<AreaAcopioEntity>> GetAreaAcopio(string nlote)
        {
            var areaAcopio = await _context.AreaAcopio.FindAsync(nlote);

            if (areaAcopio == null)
            {
                return NotFound();
            }

            return areaAcopio;
        }

        // PUT: api/AreaAcopio/LOTE-001
        [HttpPut("{nlote}")]
        public async Task<IActionResult> PutAreaAcopio(string nlote, AreaAcopioEntity areaAcopio)
        {
            if (nlote != areaAcopio.Nlote)
            {
                return BadRequest();
            }

            _context.Entry(areaAcopio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AreaAcopioExists(nlote))
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

        // POST: api/AreaAcopio
        [HttpPost]
        public async Task<ActionResult<AreaAcopioEntity>> PostAreaAcopio(AreaAcopioEntity areaAcopio)
        {
            try
            {
                _context.AreaAcopio.Add(areaAcopio);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAreaAcopio), new { nlote = areaAcopio.Nlote }, areaAcopio);
            }
            catch (DbUpdateException)
            {
                if (AreaAcopioExists(areaAcopio.Nlote))
                {
                    return Conflict(new { message = "El lote ya existe" });
                }
                throw;
            }
        }

        // DELETE: api/AreaAcopio/LOTE-001
        [HttpDelete("{nlote}")]
        public async Task<IActionResult> DeleteAreaAcopio(string nlote)
        {
            var areaAcopio = await _context.AreaAcopio.FindAsync(nlote);
            if (areaAcopio == null)
            {
                return NotFound();
            }

            _context.AreaAcopio.Remove(areaAcopio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AreaAcopioExists(string nlote)
        {
            return _context.AreaAcopio.Any(e => e.Nlote == nlote);
        }
    }
}
