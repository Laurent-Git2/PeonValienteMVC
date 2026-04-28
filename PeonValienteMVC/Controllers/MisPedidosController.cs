using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeonValienteMVC.Data;
using PeonValienteMVC.Models;

namespace PeonValienteMVC.Controllers
{
    [Authorize]
    public class MisPedidosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public MisPedidosController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var email = User.Identity.Name;

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.Email == email);

            if (cliente == null)
                return View(new List<Pedido>());

            var pedidos = await _context.Pedidos
            .Include(p => p.Estado)
            .Include(p => p.Detalles)
            .Where(p => p.ClienteId == cliente.Id)
            .OrderByDescending(p => p.Id)
            .ToListAsync();

            return View(pedidos);
        }
        public async Task<IActionResult> Detalles(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Estado)
                .Include(p => p.Detalles)
                    .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
                return NotFound();

            return View(pedido);
        }
    }
}