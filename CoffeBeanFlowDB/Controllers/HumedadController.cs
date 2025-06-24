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
    public class HumedadController : Controller
    {
        private readonly HumedadContext _context;

        public HumedadController(HumedadContext context)
        {
            _context = context;
        }

        // GET: Humedad
        public async Task<IActionResult> Index()
        {
            return View(await _context.Humedad.ToListAsync());
        }

        // GET: Humedad/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var humedadItem = await _context.Humedad
                .FirstOrDefaultAsync(m => m.ID_Humedad == id);
            if (humedadItem == null)
            {
                return NotFound();
            }

            return View(humedadItem);
        }

        // GET: Humedad/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Humedad/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Humedad,PHumedad,Temperatura,ID_Secado")] HumedadItem humedadItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(humedadItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(humedadItem);
        }

        // GET: Humedad/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var humedadItem = await _context.Humedad.FindAsync(id);
            if (humedadItem == null)
            {
                return NotFound();
            }
            return View(humedadItem);
        }

        // POST: Humedad/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Humedad,PHumedad,Temperatura,ID_Secado")] HumedadItem humedadItem)
        {
            if (id != humedadItem.ID_Humedad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(humedadItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HumedadItemExists(humedadItem.ID_Humedad))
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
            return View(humedadItem);
        }

        // GET: Humedad/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var humedadItem = await _context.Humedad
                .FirstOrDefaultAsync(m => m.ID_Humedad == id);
            if (humedadItem == null)
            {
                return NotFound();
            }

            return View(humedadItem);
        }

        // POST: Humedad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var humedadItem = await _context.Humedad.FindAsync(id);
            if (humedadItem != null)
            {
                _context.Humedad.Remove(humedadItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HumedadItemExists(int id)
        {
            return _context.Humedad.Any(e => e.ID_Humedad == id);
        }
    }
}
