using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeBeanFlowDB.Contexts;
using CoffeBeanFlowDB.Models;

namespace CoffeBeanFlowDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HumedadApiController : ControllerBase
    {
        private readonly HumedadContext _context;

        public HumedadApiController(HumedadContext context)
        {
            _context = context;
        }

        // GET: api/HumedadApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HumedadItem>>> GetAll()
        {
            return await _context.Humedad.ToListAsync();
        }

        // GET: api/HumedadApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HumedadItem>> GetById(int id)
        {
            var item = await _context.Humedad.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // POST: api/HumedadApi
        [HttpPost]
        public async Task<ActionResult<HumedadItem>> Create(HumedadItem item)
        {
            _context.Humedad.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = item.ID_Humedad }, item);
        }

        // PUT: api/HumedadApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, HumedadItem item)
        {
            if (id != item.ID_Humedad)
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
                if (!HumedadItemExists(id))
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

        // DELETE: api/HumedadApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Humedad.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Humedad.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HumedadItemExists(int id)
        {
            return _context.Humedad.Any(e => e.ID_Humedad == id);
        }
    }
}
