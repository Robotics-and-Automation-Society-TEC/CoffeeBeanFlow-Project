using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeBeanFlowDB.Contexts;
using CoffeBeanFlowDB.Models;

namespace CoffeBeanFlowDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Area_AcopioController : ControllerBase
    {
        private readonly Area_AcopioContext _context;

        public Area_AcopioController(Area_AcopioContext context)
        {
            _context = context;
        }

        // GET: api/Area_Acopio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Area_AcopioItem>>> GetArea_Acopio()
        {
            return await _context.Area_Acopio.ToListAsync();
        }

        // GET: api/Area_Acopio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Area_AcopioItem>> GetArea_AcopioItem(string id)
        {
            var area_AcopioItem = await _context.Area_Acopio
                .FirstOrDefaultAsync(m => m.Nlote == id);

            if (area_AcopioItem == null)
            {
                return NotFound();
            }

            return area_AcopioItem;
        }

        // POST: api/Area_Acopio
        [HttpPost]
        public async Task<ActionResult<Area_AcopioItem>> PostArea_AcopioItem(Area_AcopioItem area_AcopioItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Area_Acopio.Add(area_AcopioItem);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Area_AcopioItemExists(area_AcopioItem.Nlote))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetArea_AcopioItem), new { id = area_AcopioItem.Nlote }, area_AcopioItem);
        }

        // PUT: api/Area_Acopio/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArea_AcopioItem(string id, Area_AcopioItem area_AcopioItem)
        {
            if (id != area_AcopioItem.Nlote)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(area_AcopioItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Area_AcopioItemExists(id))
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

        // DELETE: api/Area_Acopio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArea_AcopioItem(string id)
        {
            var area_AcopioItem = await _context.Area_Acopio.FindAsync(id);
            if (area_AcopioItem == null)
            {
                return NotFound();
            }

            _context.Area_Acopio.Remove(area_AcopioItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Area_AcopioItemExists(string id)
        {
            return _context.Area_Acopio.Any(e => e.Nlote == id);
        }
    }
}