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
    public class Gbx_sobremadurasController : Controller
    {
        private readonly Gbx_sobremadurasContext _context;

        public Gbx_sobremadurasController(Gbx_sobremadurasContext context)
        {
            _context = context;
        }

        // GET: Gbx_sobremaduras
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gbx_sobremaduras.ToListAsync());
        }

        // GET: Gbx_sobremaduras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gbx_sobremadurasItem = await _context.Gbx_sobremaduras
                .FirstOrDefaultAsync(m => m.ID_Gbx_sobremaduras == id);
            if (gbx_sobremadurasItem == null)
            {
                return NotFound();
            }

            return View(gbx_sobremadurasItem);
        }

        // GET: Gbx_sobremaduras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gbx_sobremaduras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Gbx_sobremaduras,Valor,ID_sobremaduras")] Gbx_sobremadurasItem gbx_sobremadurasItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gbx_sobremadurasItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gbx_sobremadurasItem);
        }

        // GET: Gbx_sobremaduras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gbx_sobremadurasItem = await _context.Gbx_sobremaduras.FindAsync(id);
            if (gbx_sobremadurasItem == null)
            {
                return NotFound();
            }
            return View(gbx_sobremadurasItem);
        }

        // POST: Gbx_sobremaduras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Gbx_sobremaduras,Valor,ID_sobremaduras")] Gbx_sobremadurasItem gbx_sobremadurasItem)
        {
            if (id != gbx_sobremadurasItem.ID_Gbx_sobremaduras)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gbx_sobremadurasItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Gbx_sobremadurasItemExists(gbx_sobremadurasItem.ID_Gbx_sobremaduras))
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
            return View(gbx_sobremadurasItem);
        }

        // GET: Gbx_sobremaduras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gbx_sobremadurasItem = await _context.Gbx_sobremaduras
                .FirstOrDefaultAsync(m => m.ID_Gbx_sobremaduras == id);
            if (gbx_sobremadurasItem == null)
            {
                return NotFound();
            }

            return View(gbx_sobremadurasItem);
        }

        // POST: Gbx_sobremaduras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gbx_sobremadurasItem = await _context.Gbx_sobremaduras.FindAsync(id);
            if (gbx_sobremadurasItem != null)
            {
                _context.Gbx_sobremaduras.Remove(gbx_sobremadurasItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Gbx_sobremadurasItemExists(int id)
        {
            return _context.Gbx_sobremaduras.Any(e => e.ID_Gbx_sobremaduras == id);
        }
    }
}
