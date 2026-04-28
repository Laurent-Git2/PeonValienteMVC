using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using PeonValienteMVC.Data;
using PeonValienteMVC.Models;

namespace PeonValienteMVC.Controllers
{

    public class EscaparateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EscaparateController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id, int page = 1)
        {

            ViewData["Categorias"] = await _context.Categorias
                .OrderBy(c => c.Descripcion)
                .ToListAsync();

            var productos = _context.Productos
                .Include(p => p.Categoria)
                .Where(p => p.Escaparate == true)
                .AsQueryable();

            if (id != null)
            {
                productos = productos.Where(p => p.CategoriaId == id);
            }
            productos = productos.OrderByDescending(p => p.Precio);
            return View(await productos.ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> AgregarCarrito(int id)
        {
            var producto = await _context.Productos
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null)
                return NotFound();

            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AgregarCarritoPost(int id)
        {
            // Cargar datos de producto a añadir al carrito
            var producto = await _context.Productos
            .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }
            // Crear nuevo pedido, si el carrito está vacío y, por tanto, no existe pedido actual
            // La variable de sesión NumPedido almacena el número de pedido del carrito
            //if (string.IsNullOrEmpty(HttpContext.Session.GetString("NumPedido")) )
            if (HttpContext.Session.GetString("NumPedido") == null)
            {
                // Crear objeto pedido a agregar
                Pedido pedido = new Pedido();
                pedido.Fecha = DateTime.Now;
                pedido.Confirmado = null;
                pedido.Preparado = null;
                pedido.Enviado = null;
                pedido.Cobrado = null;
                pedido.Devuelto = null;
                pedido.Anulado = null;
                var email = User.Identity.Name;

                var cliente = await _context.Clientes
                    .FirstOrDefaultAsync(c => c.Email == email);

                if (cliente == null)
                {
                    TempData["Mensaje"] = "Complete sus datos antes de comprar.";

                    return RedirectToAction("Edit", "MisDatos");
                }

                pedido.ClienteId = cliente.Id;
                pedido.EstadoId = 1;



                _context.Add(pedido);
                    await _context.SaveChangesAsync();
                
                // Se asigna el número de pedido a la variable de sesión
                // que almacena el número de pedido del carrito
                HttpContext.Session.SetString("NumPedido", pedido.Id.ToString());
            }
          
           

            // Crear objeto detalle para agregar el producto al detalle del pedido del carrito
            DetallePedido detalle = new DetallePedido();

            string strNumeroPedido = HttpContext.Session.GetString("NumPedido");
            detalle.PedidoId = Convert.ToInt32(strNumeroPedido);
            detalle.ProductoId = id; // El valor id tiene el id del producto a agregar
            detalle.Cantidad = 1;
            detalle.Precio = producto.Precio;
            detalle.Descuento = 0;

           
                _context.Add(detalle);
                await _context.SaveChangesAsync();
            
            return RedirectToAction("Index", "Carrito");
        }
    }
    
    
}