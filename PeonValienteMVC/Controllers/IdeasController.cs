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
    public class IdeasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IdeasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ideas
        // GET: Ideas
        public async Task<IActionResult> Index()
        {
            var ideas = _context.Ideas
                .Include(i => i.Coleccion);

            return View(await ideas.ToListAsync());
        }

        // GET: Ideas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var idea = await _context.Ideas
     .Include(i => i.Coleccion)
     .FirstOrDefaultAsync(m => m.Id == id);
            if (idea == null)
            {
                return NotFound();
            }

            return View(idea);
        }

        // GET: Ideas/Create
        public IActionResult Create()
        {
            ViewData["ColeccionId"] = new SelectList(
                _context.Colecciones.OrderBy(c => c.Nombre),
                "Id",
                "Nombre");

            return View();
        }

        // POST: Ideas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
    [Bind("Id,Codigo,Titulo,FraseEspanol,FraseFrances,Descripcion,ColeccionId,TipoProducto,Potencial,Estado,ImagenTerminada,ArchivoPodTerminado,MockupTerminado,PublicadoEtsy,PublicadoPinterest,RutaImagen,FechaCreacion,Notas")]
    Idea idea)
        {
            if (ModelState.IsValid)
            {
                _context.Add(idea);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["ColeccionId"] = new SelectList(
                _context.Colecciones.OrderBy(c => c.Nombre),
                "Id",
                "Nombre",
                idea.ColeccionId);

            return View(idea);
        }
        // GET: Ideas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var idea = await _context.Ideas.FindAsync(id);

            if (idea == null)
            {
                return NotFound();
            }

            ViewData["ColeccionId"] = new SelectList(
                _context.Colecciones.OrderBy(c => c.Nombre),
                "Id",
                "Nombre",
                idea.ColeccionId);

            return View(idea);
        }

        // POST: Ideas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Codigo,Titulo,FraseEspanol,FraseFrances,Descripcion,ColeccionId,TipoProducto,Potencial,Estado,ImagenTerminada,ArchivoPodTerminado,MockupTerminado,PublicadoEtsy,PublicadoPinterest,RutaImagen,FechaCreacion,Notas")] Idea idea)
        {
            if (id != idea.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(idea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IdeaExists(idea.Id))
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
            return View(idea);
        }

        // GET: Ideas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var idea = await _context.Ideas
             .Include(i => i.Coleccion)
             .FirstOrDefaultAsync(m => m.Id == id);
            if (idea == null)
            {
                return NotFound();
            }

            return View(idea);
        }

        // POST: Ideas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var idea = await _context.Ideas.FindAsync(id);
            if (idea != null)
            {
                _context.Ideas.Remove(idea);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IdeaExists(int id)
        {
            return _context.Ideas.Any(e => e.Id == id);
        }
    }
}
