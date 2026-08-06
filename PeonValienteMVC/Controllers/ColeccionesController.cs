using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PeonValienteMVC.Data;
using PeonValienteMVC.Models;

namespace PeonValienteMVC.Controllers
{
    public class ColeccionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ColeccionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Colecciones
        public async Task<IActionResult> Index()
        {
            return View(await _context.Colecciones.ToListAsync());
        }

        // GET: Colecciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coleccion = await _context.Colecciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coleccion == null)
            {
                return NotFound();
            }

            return View(coleccion);
        }

        // GET: Colecciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Colecciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Prefijo")] Coleccion coleccion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coleccion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coleccion);
        }

        // GET: Colecciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coleccion = await _context.Colecciones.FindAsync(id);
            if (coleccion == null)
            {
                return NotFound();
            }
            return View(coleccion);
        }

        // POST: Colecciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Prefijo")] Coleccion coleccion)
        {
            if (id != coleccion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coleccion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColeccionExists(coleccion.Id))
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
            return View(coleccion);
        }

        // GET: Colecciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coleccion = await _context.Colecciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coleccion == null)
            {
                return NotFound();
            }

            return View(coleccion);
        }

        // POST: Colecciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coleccion = await _context.Colecciones.FindAsync(id);
            if (coleccion != null)
            {
                _context.Colecciones.Remove(coleccion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ColeccionExists(int id)
        {
            return _context.Colecciones.Any(e => e.Id == id);
        }
    }
}
