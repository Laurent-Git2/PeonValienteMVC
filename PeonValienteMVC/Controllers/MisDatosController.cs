using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PeonValienteMVC.Models;
using PeonValienteMVC.Data;
using Microsoft.EntityFrameworkCore;

namespace PeonValienteMVC.Controllers
{
    [Authorize]
    public class MisDatosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MisDatosController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: MisDatos/Crear
        public IActionResult Create()
        {
            Cliente cliente = new Cliente();

            cliente.Email = User.Identity.Name;

            return View(cliente);
        }

        // GET: MisDatos/Editar
        public async Task<IActionResult> Edit()
        {
            string email = User.Identity.Name;

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.Email == email);

            if (cliente == null)
            {
                return RedirectToAction("Create");
            }

            return View(cliente);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            cliente.Email = User.Identity.Name;

            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Escaparate");
            }

            return View(cliente);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Cliente cliente)
        {
            cliente.Email = User.Identity.Name;

            if (ModelState.IsValid)
            {
                _context.Update(cliente);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Escaparate");
            }

            return View(cliente);
        }
    }
}