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
    public class TemperaturaSecadoController : Controller
    {
        private readonly TemperaturaSecadoContext _context;

        public TemperaturaSecadoController(TemperaturaSecadoContext context)
        {
            _context = context;
        }

        // GET: TemperaturaSecado
        public async Task<IActionResult> Index()
        {
            return View(await _context.TemperaturaSecado.ToListAsync());
        }

        // GET: TemperaturaSecado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var temperaturaSecadoItem = await _context.TemperaturaSecado
                .FirstOrDefaultAsync(m => m.ID_Temperatura == id);
            if (temperaturaSecadoItem == null)
            {
                return NotFound();
            }

            return View(temperaturaSecadoItem);
        }

        // GET: TemperaturaSecado/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TemperaturaSecado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Temperatura,Lectura,ID_Secado")] TemperaturaSecadoItem temperaturaSecadoItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(temperaturaSecadoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(temperaturaSecadoItem);
        }

        // GET: TemperaturaSecado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var temperaturaSecadoItem = await _context.TemperaturaSecado.FindAsync(id);
            if (temperaturaSecadoItem == null)
            {
                return NotFound();
            }
            return View(temperaturaSecadoItem);
        }

        // POST: TemperaturaSecado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Temperatura,Lectura,ID_Secado")] TemperaturaSecadoItem temperaturaSecadoItem)
        {
            if (id != temperaturaSecadoItem.ID_Temperatura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(temperaturaSecadoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TemperaturaSecadoItemExists(temperaturaSecadoItem.ID_Temperatura))
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
            return View(temperaturaSecadoItem);
        }

        // GET: TemperaturaSecado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var temperaturaSecadoItem = await _context.TemperaturaSecado
                .FirstOrDefaultAsync(m => m.ID_Temperatura == id);
            if (temperaturaSecadoItem == null)
            {
                return NotFound();
            }

            return View(temperaturaSecadoItem);
        }

        // POST: TemperaturaSecado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var temperaturaSecadoItem = await _context.TemperaturaSecado.FindAsync(id);
            if (temperaturaSecadoItem != null)
            {
                _context.TemperaturaSecado.Remove(temperaturaSecadoItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TemperaturaSecadoItemExists(int id)
        {
            return _context.TemperaturaSecado.Any(e => e.ID_Temperatura == id);
        }
    }
}
