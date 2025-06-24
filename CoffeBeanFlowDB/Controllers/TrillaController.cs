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
    public class TrillaController : Controller
    {
        private readonly TrillaContext _context;

        public TrillaController(TrillaContext context)
        {
            _context = context;
        }

        // GET: Trilla
        public async Task<IActionResult> Index()
        {
            return View(await _context.Trilla.ToListAsync());
        }

        // GET: Trilla/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trillaItem = await _context.Trilla
                .FirstOrDefaultAsync(m => m.ID_Trilla == id);
            if (trillaItem == null)
            {
                return NotFound();
            }

            return View(trillaItem);
        }

        // GET: Trilla/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trilla/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Trilla,Psegundas,Pmenudos,Pinferiores,Pmadres,Pprimera,Pcaracolillo,Pescogeduras,Pbarreduras,Pcataduras,POinferiores,RTeoricoSeleccion,RTeoricoPelado,RfinalPelado,RfinalSeleccion,WVerdeFinal,WVerdeTeorico,FFinalReposo,HFinal,HInicial,Nlote")] TrillaItem trillaItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trillaItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trillaItem);
        }

        // GET: Trilla/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trillaItem = await _context.Trilla.FindAsync(id);
            if (trillaItem == null)
            {
                return NotFound();
            }
            return View(trillaItem);
        }

        // POST: Trilla/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Trilla,Psegundas,Pmenudos,Pinferiores,Pmadres,Pprimera,Pcaracolillo,Pescogeduras,Pbarreduras,Pcataduras,POinferiores,RTeoricoSeleccion,RTeoricoPelado,RfinalPelado,RfinalSeleccion,WVerdeFinal,WVerdeTeorico,FFinalReposo,HFinal,HInicial,Nlote")] TrillaItem trillaItem)
        {
            if (id != trillaItem.ID_Trilla)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trillaItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrillaItemExists(trillaItem.ID_Trilla))
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
            return View(trillaItem);
        }

        // GET: Trilla/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trillaItem = await _context.Trilla
                .FirstOrDefaultAsync(m => m.ID_Trilla == id);
            if (trillaItem == null)
            {
                return NotFound();
            }

            return View(trillaItem);
        }

        // POST: Trilla/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trillaItem = await _context.Trilla.FindAsync(id);
            if (trillaItem != null)
            {
                _context.Trilla.Remove(trillaItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrillaItemExists(int id)
        {
            return _context.Trilla.Any(e => e.ID_Trilla == id);
        }
    }
}
