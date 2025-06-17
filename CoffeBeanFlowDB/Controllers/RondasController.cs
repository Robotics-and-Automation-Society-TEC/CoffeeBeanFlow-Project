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
    public class RondasController : Controller
    {
        private readonly RondasContext _context;

        public RondasController(RondasContext context)
        {
            _context = context;
        }

        // GET: Rondas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rondas.ToListAsync());
        }

        // GET: Rondas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rondasItem = await _context.Rondas
                .FirstOrDefaultAsync(m => m.ID_Rondas == id);
            if (rondasItem == null)
            {
                return NotFound();
            }

            return View(rondasItem);
        }

        // GET: Rondas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rondas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Rondas,Valor_calidad,ID_catacion")] RondasItem rondasItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rondasItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rondasItem);
        }

        // GET: Rondas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rondasItem = await _context.Rondas.FindAsync(id);
            if (rondasItem == null)
            {
                return NotFound();
            }
            return View(rondasItem);
        }

        // POST: Rondas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Rondas,Valor_calidad,ID_catacion")] RondasItem rondasItem)
        {
            if (id != rondasItem.ID_Rondas)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rondasItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RondasItemExists(rondasItem.ID_Rondas))
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
            return View(rondasItem);
        }

        // GET: Rondas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rondasItem = await _context.Rondas
                .FirstOrDefaultAsync(m => m.ID_Rondas == id);
            if (rondasItem == null)
            {
                return NotFound();
            }

            return View(rondasItem);
        }

        // POST: Rondas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rondasItem = await _context.Rondas.FindAsync(id);
            if (rondasItem != null)
            {
                _context.Rondas.Remove(rondasItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RondasItemExists(int id)
        {
            return _context.Rondas.Any(e => e.ID_Rondas == id);
        }
    }
}
