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
    public class Guardar_CafeController : Controller
    {
        private readonly Guardar_CafeContext _context;

        public Guardar_CafeController(Guardar_CafeContext context)
        {
            _context = context;
        }

        // GET: Guardar_Cafe
        public async Task<IActionResult> Index()
        {
            return View(await _context.Guardar_Cafe.ToListAsync());
        }

        // GET: Guardar_Cafe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guardar_CafeItem = await _context.Guardar_Cafe
                .FirstOrDefaultAsync(m => m.ID_Secado == id);
            if (guardar_CafeItem == null)
            {
                return NotFound();
            }

            return View(guardar_CafeItem);
        }

        // GET: Guardar_Cafe/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Guardar_Cafe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Secado,ID_Bodega")] Guardar_CafeItem guardar_CafeItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guardar_CafeItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(guardar_CafeItem);
        }

        // GET: Guardar_Cafe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guardar_CafeItem = await _context.Guardar_Cafe.FindAsync(id);
            if (guardar_CafeItem == null)
            {
                return NotFound();
            }
            return View(guardar_CafeItem);
        }

        // POST: Guardar_Cafe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Secado,ID_Bodega")] Guardar_CafeItem guardar_CafeItem)
        {
            if (id != guardar_CafeItem.ID_Secado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guardar_CafeItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Guardar_CafeItemExists(guardar_CafeItem.ID_Secado))
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
            return View(guardar_CafeItem);
        }

        // GET: Guardar_Cafe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guardar_CafeItem = await _context.Guardar_Cafe
                .FirstOrDefaultAsync(m => m.ID_Secado == id);
            if (guardar_CafeItem == null)
            {
                return NotFound();
            }

            return View(guardar_CafeItem);
        }

        // POST: Guardar_Cafe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guardar_CafeItem = await _context.Guardar_Cafe.FindAsync(id);
            if (guardar_CafeItem != null)
            {
                _context.Guardar_Cafe.Remove(guardar_CafeItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Guardar_CafeItemExists(int id)
        {
            return _context.Guardar_Cafe.Any(e => e.ID_Secado == id);
        }
    }
}
