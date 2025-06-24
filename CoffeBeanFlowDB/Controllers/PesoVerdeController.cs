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
    public class PesoVerdeController : Controller
    {
        private readonly PesoVerdeContext _context;

        public PesoVerdeController(PesoVerdeContext context)
        {
            _context = context;
        }

        // GET: PesoVerde
        public async Task<IActionResult> Index()
        {
            return View(await _context.PesoVerde.ToListAsync());
        }

        // GET: PesoVerde/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pesoVerdeItem = await _context.PesoVerde
                .FirstOrDefaultAsync(m => m.ID_PesoVerde == id);
            if (pesoVerdeItem == null)
            {
                return NotFound();
            }

            return View(pesoVerdeItem);
        }

        // GET: PesoVerde/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PesoVerde/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_PesoVerde,Winferiores,Wfinal,WFinferior,ID_PesoTrilla")] PesoVerdeItem pesoVerdeItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pesoVerdeItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pesoVerdeItem);
        }

        // GET: PesoVerde/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pesoVerdeItem = await _context.PesoVerde.FindAsync(id);
            if (pesoVerdeItem == null)
            {
                return NotFound();
            }
            return View(pesoVerdeItem);
        }

        // POST: PesoVerde/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_PesoVerde,Winferiores,Wfinal,WFinferior,ID_PesoTrilla")] PesoVerdeItem pesoVerdeItem)
        {
            if (id != pesoVerdeItem.ID_PesoVerde)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pesoVerdeItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PesoVerdeItemExists(pesoVerdeItem.ID_PesoVerde))
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
            return View(pesoVerdeItem);
        }

        // GET: PesoVerde/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pesoVerdeItem = await _context.PesoVerde
                .FirstOrDefaultAsync(m => m.ID_PesoVerde == id);
            if (pesoVerdeItem == null)
            {
                return NotFound();
            }

            return View(pesoVerdeItem);
        }

        // POST: PesoVerde/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pesoVerdeItem = await _context.PesoVerde.FindAsync(id);
            if (pesoVerdeItem != null)
            {
                _context.PesoVerde.Remove(pesoVerdeItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PesoVerdeItemExists(int id)
        {
            return _context.PesoVerde.Any(e => e.ID_PesoVerde == id);
        }
    }
}
