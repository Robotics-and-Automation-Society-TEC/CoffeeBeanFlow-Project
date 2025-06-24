using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeBeanFlowDB.Contexts;
using CoffeBeanFlowDB.Models;


namespace CoffeBeanFlowDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RondasApiController : ControllerBase
    {
        private readonly RondasContext _context;

        public RondasApiController(RondasContext context)
        {
            _context = context;
        }

        // GET: api/RondasApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RondasItem>>> GetAll()
        {
            return await _context.Rondas.ToListAsync();
        }

        // GET: api/RondasApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RondasItem>> GetById(int id)
        {
            var item = await _context.Rondas.FindAsync(id);
            if (item == null)
                return NotFound();
            return item;
        }

        // POST: api/RondasApi
        [HttpPost]
        public async Task<ActionResult<RondasItem>> Create(RondasItem item)
        {
            _context.Rondas.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = item.ID_Rondas }, item);
        }

        // PUT: api/RondasApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RondasItem item)
        {
            if (id != item.ID_Rondas)
                return BadRequest();

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RondasItemExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/RondasApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Rondas.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.Rondas.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RondasItemExists(int id)
        {
            return _context.Rondas.Any(e => e.ID_Rondas == id);
        }
    }
}
