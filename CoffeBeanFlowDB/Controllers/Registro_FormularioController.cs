using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoffeBeanFlowDB.Contexts;
using CoffeBeanFlowDB.Models;

namespace CoffeBeanFlowDB.Controllers
{
    public class Registro_FormularioController : Controller
    {
        private readonly Registro_FormularioContext _context;

        public Registro_FormularioController(Registro_FormularioContext context)
        {
            _context = context;
        }

        // GET: Registro_Formulario
        public async Task<IActionResult> Index()
        {
            return View(await _context.Registro_Formulario.ToListAsync());
        }

        // GET: Registro_Formulario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registro_FormularioItem = await _context.Registro_Formulario
                .FirstOrDefaultAsync(m => m.ID_Formulario == id);
            if (registro_FormularioItem == null)
            {
                return NotFound();
            }

            return View(registro_FormularioItem);
        }

        // GET: Registro_Formulario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Registro_Formulario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Formulario,ID_sobremaduras,ID_maduras,ID_inmaduras")] Registro_FormularioItem registro_FormularioItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registro_FormularioItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(registro_FormularioItem);
        }

        // GET: Registro_Formulario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registro_FormularioItem = await _context.Registro_Formulario.FindAsync(id);
            if (registro_FormularioItem == null)
            {
                return NotFound();
            }
            return View(registro_FormularioItem);
        }

        // POST: Registro_Formulario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Formulario,ID_sobremaduras,ID_maduras,ID_inmaduras")] Registro_FormularioItem registro_FormularioItem)
        {
            if (id != registro_FormularioItem.ID_Formulario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registro_FormularioItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Registro_FormularioItemExists(registro_FormularioItem.ID_Formulario))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(registro_FormularioItem);
        }

        // GET: Registro_Formulario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registro_FormularioItem = await _context.Registro_Formulario
                .FirstOrDefaultAsync(m => m.ID_Formulario == id);
            if (registro_FormularioItem == null)
            {
                return NotFound();
            }

            return View(registro_FormularioItem);
        }

        // POST: Registro_Formulario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registro_FormularioItem = await _context.Registro_Formulario.FindAsync(id);
            if (registro_FormularioItem != null)
            {
                _context.Registro_Formulario.Remove(registro_FormularioItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Registro_FormularioItemExists(int id)
        {
            return _context.Registro_Formulario.Any(e => e.ID_Formulario == id);
        }
    }
}
