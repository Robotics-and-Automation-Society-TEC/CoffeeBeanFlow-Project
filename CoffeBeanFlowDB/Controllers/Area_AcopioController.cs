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
    public class Area_AcopioController : Controller
    {
        private readonly Area_AcopioContext _context;

        public Area_AcopioController(Area_AcopioContext context)
        {
            _context = context;
        }

        // GET: Area_Acopio
        public async Task<IActionResult> Index()
        {
            return View(await _context.Area_Acopio.ToListAsync());
        }

        // GET: Area_Acopio/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area_AcopioItem = await _context.Area_Acopio
                .FirstOrDefaultAsync(m => m.Nlote == id);
            if (area_AcopioItem == null)
            {
                return NotFound();
            }

            return View(area_AcopioItem);
        }

        // GET: Area_Acopio/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Area_Acopio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nlote,Rtotal,Robjetivo,Rsobreobjetivo,Vendido,Disponible,Enproceso,Altura,Zona,Nrecibo,Nproductor,Nfinca,Despulpado,Psegundas,PDmecanicos,PPulpaPergamino,PPergaminoPulpa,DFruta,Dpergamino_humedo,ID_Secado")] Area_AcopioItem area_AcopioItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(area_AcopioItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(area_AcopioItem);
        }

        // GET: Area_Acopio/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area_AcopioItem = await _context.Area_Acopio.FindAsync(id);
            if (area_AcopioItem == null)
            {
                return NotFound();
            }
            return View(area_AcopioItem);
        }

        // POST: Area_Acopio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Nlote,Rtotal,Robjetivo,Rsobreobjetivo,Vendido,Disponible,Enproceso,Altura,Zona,Nrecibo,Nproductor,Nfinca,Despulpado,Psegundas,PDmecanicos,PPulpaPergamino,PPergaminoPulpa,DFruta,Dpergamino_humedo,ID_Secado")] Area_AcopioItem area_AcopioItem)
        {
            if (id != area_AcopioItem.Nlote)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(area_AcopioItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Area_AcopioItemExists(area_AcopioItem.Nlote))
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
            return View(area_AcopioItem);
        }

        // GET: Area_Acopio/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area_AcopioItem = await _context.Area_Acopio
                .FirstOrDefaultAsync(m => m.Nlote == id);
            if (area_AcopioItem == null)
            {
                return NotFound();
            }

            return View(area_AcopioItem);
        }

        // POST: Area_Acopio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var area_AcopioItem = await _context.Area_Acopio.FindAsync(id);
            if (area_AcopioItem != null)
            {
                _context.Area_Acopio.Remove(area_AcopioItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Area_AcopioItemExists(string id)
        {
            return _context.Area_Acopio.Any(e => e.Nlote == id);
        }
    }
}
