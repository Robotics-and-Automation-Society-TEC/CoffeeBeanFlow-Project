using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeBeanFlowDB.Contexts;
using CoffeBeanFlowDB.Models;


namespace CoffeBeanFlowDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Registro_FormularioApiController : ControllerBase
    {
        private readonly Registro_FormularioContext _context;

        public Registro_FormularioApiController(Registro_FormularioContext context)
        {
            _context = context;
        }

        // GET: api/Registro_FormularioApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Registro_FormularioItem>>> GetAll()
        {
            return await _context.Registro_Formulario.ToListAsync();
        }

        // GET: api/Registro_FormularioApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Registro_FormularioItem>> GetById(int id)
        {
            var item = await _context.Registro_Formulario.FindAsync(id);
            if (item == null)
                return NotFound();
            return item;
        }

        // POST: api/Registro_FormularioApi
        [HttpPost]
        public async Task<ActionResult<Registro_FormularioItem>> Create(Registro_FormularioItem item)
        {
            _context.Registro_Formulario.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = item.ID_Formulario }, item);
        }

        // PUT: api/Registro_FormularioApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Registro_FormularioItem item)
        {
            if (id != item.ID_Formulario)
                return BadRequest();

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Registro_FormularioItemExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Registro_FormularioApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Registro_Formulario.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.Registro_Formulario.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Registro_FormularioItemExists(int id)
        {
            return _context.Registro_Formulario.Any(e => e.ID_Formulario == id);
        }
    }
}
