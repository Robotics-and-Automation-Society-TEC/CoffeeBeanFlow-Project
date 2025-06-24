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
    public class RCmadurasController : Controller
    {
        private readonly RCmadurasContext _context;

        public RCmadurasController(RCmadurasContext context)
        {
            _context = context;
        }

        // GET: RCmaduras
        public async Task<IActionResult> Index()
        {
            return View(await _context.RCmaduras.ToListAsync());
        }

        // GET: RCmaduras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rCmadurasItem = await _context.RCmaduras
                .FirstOrDefaultAsync(m => m.ID_maduras == id);
            if (rCmadurasItem == null)
            {
                return NotFound();
            }

            return View(rCmadurasItem);
        }

        // GET: RCmaduras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RCmaduras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_maduras,Promedio,Observaciones,Gbx,Tiempo")] RCmadurasItem rCmadurasItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rCmadurasItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rCmadurasItem);
        }

        // GET: RCmaduras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rCmadurasItem = await _context.RCmaduras.FindAsync(id);
            if (rCmadurasItem == null)
            {
                return NotFound();
            }
            return View(rCmadurasItem);
        }

        // POST: RCmaduras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_maduras,Promedio,Observaciones,Gbx,Tiempo")] RCmadurasItem rCmadurasItem)
        {
            if (id != rCmadurasItem.ID_maduras)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rCmadurasItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RCmadurasItemExists(rCmadurasItem.ID_maduras))
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
            return View(rCmadurasItem);
        }

        // GET: RCmaduras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rCmadurasItem = await _context.RCmaduras
                .FirstOrDefaultAsync(m => m.ID_maduras == id);
            if (rCmadurasItem == null)
            {
                return NotFound();
            }

            return View(rCmadurasItem);
        }

        // POST: RCmaduras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rCmadurasItem = await _context.RCmaduras.FindAsync(id);
            if (rCmadurasItem != null)
            {
                _context.RCmaduras.Remove(rCmadurasItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RCmadurasItemExists(int id)
        {
            return _context.RCmaduras.Any(e => e.ID_maduras == id);
        }
    }
}
