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
    public class BodegaController : Controller
    {
        private readonly BodegaContext _context;

        public BodegaController(BodegaContext context)
        {
            _context = context;
        }

        // GET: Bodega
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bodega.ToListAsync());
        }

        // GET: Bodega/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bodegaItem = await _context.Bodega
                .FirstOrDefaultAsync(m => m.ID_Bodega == id);
            if (bodegaItem == null)
            {
                return NotFound();
            }

            return View(bodegaItem);
        }

        // GET: Bodega/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bodega/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Bodega,D_Bellota,D_Pergamino,Hinicial,Hfinal,W_pergamino,W_bellota,FinicioReposo,CantidadSacos,PMTexterna,PMTinterna,PMH_relativa,Nlote")] BodegaItem bodegaItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bodegaItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bodegaItem);
        }

        // GET: Bodega/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bodegaItem = await _context.Bodega.FindAsync(id);
            if (bodegaItem == null)
            {
                return NotFound();
            }
            return View(bodegaItem);
        }

        // POST: Bodega/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Bodega,D_Bellota,D_Pergamino,Hinicial,Hfinal,W_pergamino,W_bellota,FinicioReposo,CantidadSacos,PMTexterna,PMTinterna,PMH_relativa,Nlote")] BodegaItem bodegaItem)
        {
            if (id != bodegaItem.ID_Bodega)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bodegaItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BodegaItemExists(bodegaItem.ID_Bodega))
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
            return View(bodegaItem);
        }

        // GET: Bodega/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bodegaItem = await _context.Bodega
                .FirstOrDefaultAsync(m => m.ID_Bodega == id);
            if (bodegaItem == null)
            {
                return NotFound();
            }

            return View(bodegaItem);
        }

        // POST: Bodega/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bodegaItem = await _context.Bodega.FindAsync(id);
            if (bodegaItem != null)
            {
                _context.Bodega.Remove(bodegaItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BodegaItemExists(int id)
        {
            return _context.Bodega.Any(e => e.ID_Bodega == id);
        }
    }
}
