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
    public class SuministraController : Controller
    {
        private readonly SuministraContext _context;

        public SuministraController(SuministraContext context)
        {
            _context = context;
        }

        // GET: Suministra
        public async Task<IActionResult> Index()
        {
            return View(await _context.Suministra.ToListAsync());
        }

        // GET: Suministra/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suministraItem = await _context.Suministra
                .FirstOrDefaultAsync(m => m.ID_Bodega == id);
            if (suministraItem == null)
            {
                return NotFound();
            }

            return View(suministraItem);
        }

        // GET: Suministra/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suministra/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Bodega,ID_Trilla")] SuministraItem suministraItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(suministraItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(suministraItem);
        }

        // GET: Suministra/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suministraItem = await _context.Suministra.FindAsync(id);
            if (suministraItem == null)
            {
                return NotFound();
            }
            return View(suministraItem);
        }

        // POST: Suministra/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Bodega,ID_Trilla")] SuministraItem suministraItem)
        {
            if (id != suministraItem.ID_Bodega)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(suministraItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuministraItemExists(suministraItem.ID_Bodega))
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
            return View(suministraItem);
        }

        // GET: Suministra/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suministraItem = await _context.Suministra
                .FirstOrDefaultAsync(m => m.ID_Bodega == id);
            if (suministraItem == null)
            {
                return NotFound();
            }

            return View(suministraItem);
        }

        // POST: Suministra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var suministraItem = await _context.Suministra.FindAsync(id);
            if (suministraItem != null)
            {
                _context.Suministra.Remove(suministraItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuministraItemExists(int id)
        {
            return _context.Suministra.Any(e => e.ID_Bodega == id);
        }
    }
}
