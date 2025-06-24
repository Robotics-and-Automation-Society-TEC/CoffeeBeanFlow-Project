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
    public class NcamaController : Controller
    {
        private readonly NcamaContext _context;

        public NcamaController(NcamaContext context)
        {
            _context = context;
        }

        // GET: Ncama
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ncama.ToListAsync());
        }

        // GET: Ncama/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ncamaItem = await _context.Ncama
                .FirstOrDefaultAsync(m => m.ID_Ncama == id);
            if (ncamaItem == null)
            {
                return NotFound();
            }

            return View(ncamaItem);
        }

        // GET: Ncama/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ncama/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Ncama,ID_Secado")] NcamaItem ncamaItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ncamaItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ncamaItem);
        }

        // GET: Ncama/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ncamaItem = await _context.Ncama.FindAsync(id);
            if (ncamaItem == null)
            {
                return NotFound();
            }
            return View(ncamaItem);
        }

        // POST: Ncama/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Ncama,ID_Secado")] NcamaItem ncamaItem)
        {
            if (id != ncamaItem.ID_Ncama)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ncamaItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NcamaItemExists(ncamaItem.ID_Ncama))
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
            return View(ncamaItem);
        }

        // GET: Ncama/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ncamaItem = await _context.Ncama
                .FirstOrDefaultAsync(m => m.ID_Ncama == id);
            if (ncamaItem == null)
            {
                return NotFound();
            }

            return View(ncamaItem);
        }

        // POST: Ncama/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ncamaItem = await _context.Ncama.FindAsync(id);
            if (ncamaItem != null)
            {
                _context.Ncama.Remove(ncamaItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NcamaItemExists(int id)
        {
            return _context.Ncama.Any(e => e.ID_Ncama == id);
        }
    }
}
