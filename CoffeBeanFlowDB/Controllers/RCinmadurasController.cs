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
    public class RCinmadurasController : Controller
    {
        private readonly RCinmadurasContext _context;

        public RCinmadurasController(RCinmadurasContext context)
        {
            _context = context;
        }

        // GET: RCinmaduras
        public async Task<IActionResult> Index()
        {
            return View(await _context.RCinmaduras.ToListAsync());
        }

        // GET: RCinmaduras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rCinmadurasItem = await _context.RCinmaduras
                .FirstOrDefaultAsync(m => m.ID_inmaduras == id);
            if (rCinmadurasItem == null)
            {
                return NotFound();
            }

            return View(rCinmadurasItem);
        }

        // GET: RCinmaduras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RCinmaduras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_inmaduras,Promedio,Observaciones,Gbx,Tiempo")] RCinmadurasItem rCinmadurasItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rCinmadurasItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rCinmadurasItem);
        }

        // GET: RCinmaduras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rCinmadurasItem = await _context.RCinmaduras.FindAsync(id);
            if (rCinmadurasItem == null)
            {
                return NotFound();
            }
            return View(rCinmadurasItem);
        }

        // POST: RCinmaduras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_inmaduras,Promedio,Observaciones,Gbx,Tiempo")] RCinmadurasItem rCinmadurasItem)
        {
            if (id != rCinmadurasItem.ID_inmaduras)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rCinmadurasItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RCinmadurasItemExists(rCinmadurasItem.ID_inmaduras))
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
            return View(rCinmadurasItem);
        }

        // GET: RCinmaduras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rCinmadurasItem = await _context.RCinmaduras
                .FirstOrDefaultAsync(m => m.ID_inmaduras == id);
            if (rCinmadurasItem == null)
            {
                return NotFound();
            }

            return View(rCinmadurasItem);
        }

        // POST: RCinmaduras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rCinmadurasItem = await _context.RCinmaduras.FindAsync(id);
            if (rCinmadurasItem != null)
            {
                _context.RCinmaduras.Remove(rCinmadurasItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RCinmadurasItemExists(int id)
        {
            return _context.RCinmaduras.Any(e => e.ID_inmaduras == id);
        }
    }
}
