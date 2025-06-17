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
    public class Gbx_madurasController : Controller
    {
        private readonly Gbx_madurasContext _context;

        public Gbx_madurasController(Gbx_madurasContext context)
        {
            _context = context;
        }

        // GET: Gbx_maduras
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gbx_maduras.ToListAsync());
        }

        // GET: Gbx_maduras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gbx_madurasItem = await _context.Gbx_maduras
                .FirstOrDefaultAsync(m => m.ID_Gbx_maduras == id);
            if (gbx_madurasItem == null)
            {
                return NotFound();
            }

            return View(gbx_madurasItem);
        }

        // GET: Gbx_maduras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gbx_maduras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Gbx_maduras,Valor,ID_maduras")] Gbx_madurasItem gbx_madurasItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gbx_madurasItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gbx_madurasItem);
        }

        // GET: Gbx_maduras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gbx_madurasItem = await _context.Gbx_maduras.FindAsync(id);
            if (gbx_madurasItem == null)
            {
                return NotFound();
            }
            return View(gbx_madurasItem);
        }

        // POST: Gbx_maduras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Gbx_maduras,Valor,ID_maduras")] Gbx_madurasItem gbx_madurasItem)
        {
            if (id != gbx_madurasItem.ID_Gbx_maduras)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gbx_madurasItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Gbx_madurasItemExists(gbx_madurasItem.ID_Gbx_maduras))
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
            return View(gbx_madurasItem);
        }

        // GET: Gbx_maduras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gbx_madurasItem = await _context.Gbx_maduras
                .FirstOrDefaultAsync(m => m.ID_Gbx_maduras == id);
            if (gbx_madurasItem == null)
            {
                return NotFound();
            }

            return View(gbx_madurasItem);
        }

        // POST: Gbx_maduras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gbx_madurasItem = await _context.Gbx_maduras.FindAsync(id);
            if (gbx_madurasItem != null)
            {
                _context.Gbx_maduras.Remove(gbx_madurasItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Gbx_madurasItemExists(int id)
        {
            return _context.Gbx_maduras.Any(e => e.ID_Gbx_maduras == id);
        }
    }
}
