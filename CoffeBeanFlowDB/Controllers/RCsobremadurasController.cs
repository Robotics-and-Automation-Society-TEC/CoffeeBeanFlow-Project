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
    public class RCsobremadurasController : Controller
    {
        private readonly RCsobremadurasContext _context;

        public RCsobremadurasController(RCsobremadurasContext context)
        {
            _context = context;
        }

        // GET: RCsobremaduras
        public async Task<IActionResult> Index()
        {
            return View(await _context.RCsobremaduras.ToListAsync());
        }

        // GET: RCsobremaduras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rCsobremadurasItem = await _context.RCsobremaduras
                .FirstOrDefaultAsync(m => m.ID_sobremaduras == id);
            if (rCsobremadurasItem == null)
            {
                return NotFound();
            }

            return View(rCsobremadurasItem);
        }

        // GET: RCsobremaduras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RCsobremaduras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_sobremaduras,Promedio,Observaciones,Gbx,Tiempo")] RCsobremadurasItem rCsobremadurasItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rCsobremadurasItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rCsobremadurasItem);
        }

        // GET: RCsobremaduras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rCsobremadurasItem = await _context.RCsobremaduras.FindAsync(id);
            if (rCsobremadurasItem == null)
            {
                return NotFound();
            }
            return View(rCsobremadurasItem);
        }

        // POST: RCsobremaduras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_sobremaduras,Promedio,Observaciones,Gbx,Tiempo")] RCsobremadurasItem rCsobremadurasItem)
        {
            if (id != rCsobremadurasItem.ID_sobremaduras)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rCsobremadurasItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RCsobremadurasItemExists(rCsobremadurasItem.ID_sobremaduras))
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
            return View(rCsobremadurasItem);
        }

        // GET: RCsobremaduras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rCsobremadurasItem = await _context.RCsobremaduras
                .FirstOrDefaultAsync(m => m.ID_sobremaduras == id);
            if (rCsobremadurasItem == null)
            {
                return NotFound();
            }

            return View(rCsobremadurasItem);
        }

        // POST: RCsobremaduras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rCsobremadurasItem = await _context.RCsobremaduras.FindAsync(id);
            if (rCsobremadurasItem != null)
            {
                _context.RCsobremaduras.Remove(rCsobremadurasItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RCsobremadurasItemExists(int id)
        {
            return _context.RCsobremaduras.Any(e => e.ID_sobremaduras == id);
        }
    }
}
