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
    public class TermoHigrometriaController : Controller
    {
        private readonly TermoHigrometriaContext _context;

        public TermoHigrometriaController(TermoHigrometriaContext context)
        {
            _context = context;
        }

        // GET: TermoHigrometria
        public async Task<IActionResult> Index()
        {
            return View(await _context.TermoHigrometria.ToListAsync());
        }

        // GET: TermoHigrometria/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var termoHigrometriaItem = await _context.TermoHigrometria
                .FirstOrDefaultAsync(m => m.ID_Termo == id);
            if (termoHigrometriaItem == null)
            {
                return NotFound();
            }

            return View(termoHigrometriaItem);
        }

        // GET: TermoHigrometria/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TermoHigrometria/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Termo,Hrelativa,Tinterna,Texterna,ID_Secado")] TermoHigrometriaItem termoHigrometriaItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(termoHigrometriaItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(termoHigrometriaItem);
        }

        // GET: TermoHigrometria/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var termoHigrometriaItem = await _context.TermoHigrometria.FindAsync(id);
            if (termoHigrometriaItem == null)
            {
                return NotFound();
            }
            return View(termoHigrometriaItem);
        }

        // POST: TermoHigrometria/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Termo,Hrelativa,Tinterna,Texterna,ID_Secado")] TermoHigrometriaItem termoHigrometriaItem)
        {
            if (id != termoHigrometriaItem.ID_Termo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(termoHigrometriaItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TermoHigrometriaItemExists(termoHigrometriaItem.ID_Termo))
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
            return View(termoHigrometriaItem);
        }

        // GET: TermoHigrometria/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var termoHigrometriaItem = await _context.TermoHigrometria
                .FirstOrDefaultAsync(m => m.ID_Termo == id);
            if (termoHigrometriaItem == null)
            {
                return NotFound();
            }

            return View(termoHigrometriaItem);
        }

        // POST: TermoHigrometria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var termoHigrometriaItem = await _context.TermoHigrometria.FindAsync(id);
            if (termoHigrometriaItem != null)
            {
                _context.TermoHigrometria.Remove(termoHigrometriaItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TermoHigrometriaItemExists(int id)
        {
            return _context.TermoHigrometria.Any(e => e.ID_Termo == id);
        }
    }
}
