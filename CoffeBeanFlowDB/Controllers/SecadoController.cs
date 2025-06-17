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
    public class SecadoController : Controller
    {
        private readonly SecadoContext _context;

        public SecadoController(SecadoContext context)
        {
            _context = context;
        }

        // GET: Secado
        public async Task<IActionResult> Index()
        {
            return View(await _context.Secado.ToListAsync());
        }

        // GET: Secado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secadoItem = await _context.Secado
                .FirstOrDefaultAsync(m => m.ID_Secado == id);
            if (secadoItem == null)
            {
                return NotFound();
            }

            return View(secadoItem);
        }

        // GET: Secado/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Secado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Secado,Finicio,Dsecado,Psolar,Pmecanico,Ffinal,Nlote")] SecadoItem secadoItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(secadoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(secadoItem);
        }

        // GET: Secado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secadoItem = await _context.Secado.FindAsync(id);
            if (secadoItem == null)
            {
                return NotFound();
            }
            return View(secadoItem);
        }

        // POST: Secado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Secado,Finicio,Dsecado,Psolar,Pmecanico,Ffinal,Nlote")] SecadoItem secadoItem)
        {
            if (id != secadoItem.ID_Secado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(secadoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SecadoItemExists(secadoItem.ID_Secado))
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
            return View(secadoItem);
        }

        // GET: Secado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secadoItem = await _context.Secado
                .FirstOrDefaultAsync(m => m.ID_Secado == id);
            if (secadoItem == null)
            {
                return NotFound();
            }

            return View(secadoItem);
        }

        // POST: Secado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var secadoItem = await _context.Secado.FindAsync(id);
            if (secadoItem != null)
            {
                _context.Secado.Remove(secadoItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SecadoItemExists(int id)
        {
            return _context.Secado.Any(e => e.ID_Secado == id);
        }
    }
}
