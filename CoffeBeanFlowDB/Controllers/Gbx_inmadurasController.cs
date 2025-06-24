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
    public class Gbx_inmadurasController : Controller
    {
        private readonly Gbx_inmadurasContext _context;

        public Gbx_inmadurasController(Gbx_inmadurasContext context)
        {
            _context = context;
        }

        // GET: Gbx_inmaduras
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gbx_inmaduras.ToListAsync());
        }

        // GET: Gbx_inmaduras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gbx_inmadurasItem = await _context.Gbx_inmaduras
                .FirstOrDefaultAsync(m => m.ID_Gbx_inmaduras == id);
            if (gbx_inmadurasItem == null)
            {
                return NotFound();
            }

            return View(gbx_inmadurasItem);
        }

        // GET: Gbx_inmaduras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gbx_inmaduras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Gbx_inmaduras,Valor,ID_inmaduras")] Gbx_inmadurasItem gbx_inmadurasItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gbx_inmadurasItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gbx_inmadurasItem);
        }

        // GET: Gbx_inmaduras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gbx_inmadurasItem = await _context.Gbx_inmaduras.FindAsync(id);
            if (gbx_inmadurasItem == null)
            {
                return NotFound();
            }
            return View(gbx_inmadurasItem);
        }

        // POST: Gbx_inmaduras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Gbx_inmaduras,Valor,ID_inmaduras")] Gbx_inmadurasItem gbx_inmadurasItem)
        {
            if (id != gbx_inmadurasItem.ID_Gbx_inmaduras)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gbx_inmadurasItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Gbx_inmadurasItemExists(gbx_inmadurasItem.ID_Gbx_inmaduras))
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
            return View(gbx_inmadurasItem);
        }

        // GET: Gbx_inmaduras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gbx_inmadurasItem = await _context.Gbx_inmaduras
                .FirstOrDefaultAsync(m => m.ID_Gbx_inmaduras == id);
            if (gbx_inmadurasItem == null)
            {
                return NotFound();
            }

            return View(gbx_inmadurasItem);
        }

        // POST: Gbx_inmaduras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gbx_inmadurasItem = await _context.Gbx_inmaduras.FindAsync(id);
            if (gbx_inmadurasItem != null)
            {
                _context.Gbx_inmaduras.Remove(gbx_inmadurasItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Gbx_inmadurasItemExists(int id)
        {
            return _context.Gbx_inmaduras.Any(e => e.ID_Gbx_inmaduras == id);
        }
    }
}
