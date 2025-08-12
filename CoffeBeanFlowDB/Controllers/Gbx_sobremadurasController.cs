using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeBeanFlowDB.Contexts;
using CoffeBeanFlowDB.Models;

namespace CoffeBeanFlowDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GbxSobremadurasApiController : ControllerBase
    {
        private readonly Gbx_sobremadurasContext _context;

        public GbxSobremadurasApiController(Gbx_sobremadurasContext context)
        {
            _context = context;
        }

        // GET: api/GbxSobremadurasApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gbx_sobremadurasItem>>> GetAll()
        {
            return await _context.Gbx_sobremaduras.ToListAsync();
        }

        // GET: api/GbxSobremadurasApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gbx_sobremadurasItem>> GetById(int id)
        {
            var item = await _context.Gbx_sobremaduras.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // POST: api/GbxSobremadurasApi
        [HttpPost]
        public async Task<ActionResult<Gbx_sobremadurasItem>> Create(Gbx_sobremadurasItem item)
        {
            _context.Gbx_sobremaduras.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = item.ID_Gbx_sobremaduras }, item);
        }

        // PUT: api/GbxSobremadurasApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Gbx_sobremadurasItem item)
        {
            if (id != item.ID_Gbx_sobremaduras)
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
                if (!ItemExists(id))
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

        // DELETE: api/GbxSobremadurasApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Gbx_sobremaduras.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Gbx_sobremaduras.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(int id)
        {
            return _context.Gbx_sobremaduras.Any(e => e.ID_Gbx_sobremaduras == id);
        }
    }
}
