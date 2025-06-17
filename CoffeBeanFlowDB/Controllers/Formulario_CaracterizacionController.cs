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
    public class Formulario_CaracterizacionController : Controller
    {
        private readonly Formulario_CaracterizacionContext _context;

        public Formulario_CaracterizacionController(Formulario_CaracterizacionContext context)
        {
            _context = context;
        }

        // GET: Formulario_Caracterizacion
        public async Task<IActionResult> Index()
        {
            return View(await _context.Formulario_Caracterizacion.ToListAsync());
        }

        // GET: Formulario_Caracterizacion/Details/5
        public async Task<IActionResult> Details(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formulario_CaracterizacionItem = await _context.Formulario_Caracterizacion
                .FirstOrDefaultAsync(m => m.Tiempo == id);
            if (formulario_CaracterizacionItem == null)
            {
                return NotFound();
            }

            return View(formulario_CaracterizacionItem);
        }

        // GET: Formulario_Caracterizacion/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Formulario_Caracterizacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tiempo,Cinmaduras,Csobremaduras,Csecas,Cobjetivo,Cverdes,PCdebajo,Proceso,DRmaduras,Mtabla,PCverdes,PCsecas,PCencima,Emaduracion,Broca,Densidad,Vanos,PCobjetivo,Secos,Nlote_AreaAcopio")] Formulario_CaracterizacionItem formulario_CaracterizacionItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formulario_CaracterizacionItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(formulario_CaracterizacionItem);
        }

        // GET: Formulario_Caracterizacion/Edit/5
        public async Task<IActionResult> Edit(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formulario_CaracterizacionItem = await _context.Formulario_Caracterizacion.FindAsync(id);
            if (formulario_CaracterizacionItem == null)
            {
                return NotFound();
            }
            return View(formulario_CaracterizacionItem);
        }

        // POST: Formulario_Caracterizacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DateTime id, [Bind("Tiempo,Cinmaduras,Csobremaduras,Csecas,Cobjetivo,Cverdes,PCdebajo,Proceso,DRmaduras,Mtabla,PCverdes,PCsecas,PCencima,Emaduracion,Broca,Densidad,Vanos,PCobjetivo,Secos,Nlote_AreaAcopio")] Formulario_CaracterizacionItem formulario_CaracterizacionItem)
        {
            if (id != formulario_CaracterizacionItem.Tiempo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formulario_CaracterizacionItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Formulario_CaracterizacionItemExists(formulario_CaracterizacionItem.Tiempo))
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
            return View(formulario_CaracterizacionItem);
        }

        // GET: Formulario_Caracterizacion/Delete/5
        public async Task<IActionResult> Delete(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formulario_CaracterizacionItem = await _context.Formulario_Caracterizacion
                .FirstOrDefaultAsync(m => m.Tiempo == id);
            if (formulario_CaracterizacionItem == null)
            {
                return NotFound();
            }

            return View(formulario_CaracterizacionItem);
        }

        // POST: Formulario_Caracterizacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DateTime id)
        {
            var formulario_CaracterizacionItem = await _context.Formulario_Caracterizacion.FindAsync(id);
            if (formulario_CaracterizacionItem != null)
            {
                _context.Formulario_Caracterizacion.Remove(formulario_CaracterizacionItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Formulario_CaracterizacionItemExists(DateTime id)
        {
            return _context.Formulario_Caracterizacion.Any(e => e.Tiempo == id);
        }
    }
}
