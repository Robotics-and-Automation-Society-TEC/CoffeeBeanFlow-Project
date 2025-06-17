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
    public class CatacionController : Controller
    {
        private readonly CatacionContext _context;

        public CatacionController(CatacionContext context)
        {
            _context = context;
        }

        // GET: Catacion
        public async Task<IActionResult> Index()
        {
            return View(await _context.Catacion.ToListAsync());
        }

        // GET: Catacion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catacionItem = await _context.Catacion
                .FirstOrDefaultAsync(m => m.ID_catacion == id);
            if (catacionItem == null)
            {
                return NotFound();
            }

            return View(catacionItem);
        }

        // GET: Catacion/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Catacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_catacion,FFreposo,Nlote,Defectuoso,Limpio,Overde,CCcverde,Quaker,Rtostado,Dtueste,CCcalidad,Pfinales,TAgtron,C1negro,C1ME,C1insectos,C1cerezaseca,C1hongos,C1agrio,C2pergamino,C2inmaduro,C2negroP,C2agrioP,C2cascara_pulpa,C2insectos,C2averanado,C2partido_cortado_mordido,C2concha,C2flotador,Trece,Catorce,Quince,Dieciseis,Diecisiete,Dieciocho,Diecinueve,Veinte,TresSobreDieciseis,Residuo")] CatacionItem catacionItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(catacionItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(catacionItem);
        }

        // GET: Catacion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catacionItem = await _context.Catacion.FindAsync(id);
            if (catacionItem == null)
            {
                return NotFound();
            }
            return View(catacionItem);
        }

        // POST: Catacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_catacion,FFreposo,Nlote,Defectuoso,Limpio,Overde,CCcverde,Quaker,Rtostado,Dtueste,CCcalidad,Pfinales,TAgtron,C1negro,C1ME,C1insectos,C1cerezaseca,C1hongos,C1agrio,C2pergamino,C2inmaduro,C2negroP,C2agrioP,C2cascara_pulpa,C2insectos,C2averanado,C2partido_cortado_mordido,C2concha,C2flotador,Trece,Catorce,Quince,Dieciseis,Diecisiete,Dieciocho,Diecinueve,Veinte,TresSobreDieciseis,Residuo")] CatacionItem catacionItem)
        {
            if (id != catacionItem.ID_catacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catacionItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatacionItemExists(catacionItem.ID_catacion))
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
            return View(catacionItem);
        }

        // GET: Catacion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catacionItem = await _context.Catacion
                .FirstOrDefaultAsync(m => m.ID_catacion == id);
            if (catacionItem == null)
            {
                return NotFound();
            }

            return View(catacionItem);
        }

        // POST: Catacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var catacionItem = await _context.Catacion.FindAsync(id);
            if (catacionItem != null)
            {
                _context.Catacion.Remove(catacionItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatacionItemExists(int id)
        {
            return _context.Catacion.Any(e => e.ID_catacion == id);
        }
    }
}
