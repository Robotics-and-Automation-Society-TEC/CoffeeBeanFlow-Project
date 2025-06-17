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
    public class Enviar_muestrasController : Controller
    {
        private readonly Enviar_muestrasContext _context;

        public Enviar_muestrasController(Enviar_muestrasContext context)
        {
            _context = context;
        }

        // GET: Enviar_muestras
        public async Task<IActionResult> Index()
        {
            return View(await _context.Enviar_muestras.ToListAsync());
        }

        // GET: Enviar_muestras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enviar_muestrasItem = await _context.Enviar_muestras
                .FirstOrDefaultAsync(m => m.ID_Trilla == id);
            if (enviar_muestrasItem == null)
            {
                return NotFound();
            }

            return View(enviar_muestrasItem);
        }

        // GET: Enviar_muestras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Enviar_muestras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Trilla,ID_Catacion")] Enviar_muestrasItem enviar_muestrasItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enviar_muestrasItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(enviar_muestrasItem);
        }

        // GET: Enviar_muestras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enviar_muestrasItem = await _context.Enviar_muestras.FindAsync(id);
            if (enviar_muestrasItem == null)
            {
                return NotFound();
            }
            return View(enviar_muestrasItem);
        }

        // POST: Enviar_muestras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Trilla,ID_Catacion")] Enviar_muestrasItem enviar_muestrasItem)
        {
            if (id != enviar_muestrasItem.ID_Trilla)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enviar_muestrasItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Enviar_muestrasItemExists(enviar_muestrasItem.ID_Trilla))
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
            return View(enviar_muestrasItem);
        }

        // GET: Enviar_muestras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enviar_muestrasItem = await _context.Enviar_muestras
                .FirstOrDefaultAsync(m => m.ID_Trilla == id);
            if (enviar_muestrasItem == null)
            {
                return NotFound();
            }

            return View(enviar_muestrasItem);
        }

        // POST: Enviar_muestras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enviar_muestrasItem = await _context.Enviar_muestras.FindAsync(id);
            if (enviar_muestrasItem != null)
            {
                _context.Enviar_muestras.Remove(enviar_muestrasItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Enviar_muestrasItemExists(int id)
        {
            return _context.Enviar_muestras.Any(e => e.ID_Trilla == id);
        }
    }
}
