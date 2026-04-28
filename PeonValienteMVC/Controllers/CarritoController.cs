using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeonValienteMVC.Data;
using PeonValienteMVC.Models;

public class CarritoController : Controller
{
    private readonly ApplicationDbContext _context;

    public CarritoController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var numPedido = HttpContext.Session.GetString("NumPedido");

        if (numPedido == null)
        {
            return RedirectToAction("CarritoVacio");
        }

        int idPedido = Convert.ToInt32(numPedido);

        var pedido = await _context.Pedidos
            .Include(p => p.Detalles)
            .ThenInclude(d => d.Producto)
            .FirstOrDefaultAsync(p => p.Id == idPedido);

        if (pedido == null || pedido.Detalles == null || !pedido.Detalles.Any())
        {
            return RedirectToAction("CarritoVacio");
        }

        return View(pedido);
    }

    public IActionResult CarritoVacio()
    {
        return View();
    }

    public async Task<IActionResult> VaciarCarrito()
    {
        var numPedido = HttpContext.Session.GetString("NumPedido");

        if (numPedido != null)
        {
            int idPedido = Convert.ToInt32(numPedido);

            var pedido = await _context.Pedidos
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.Id == idPedido);

            if (pedido != null)
            {
                _context.DetallesPedido.RemoveRange(pedido.Detalles);
                _context.Pedidos.Remove(pedido);

                await _context.SaveChangesAsync();
            }
        }

        HttpContext.Session.Remove("NumPedido");

        return RedirectToAction("CarritoVacio");
    }


    public IActionResult LimpiarSesion()
    {
        HttpContext.Session.Clear();

        return RedirectToAction("CarritoVacio");
    }

    public async Task<IActionResult> EliminarLinea(int id)
    {
        var detalle = await _context.DetallesPedido.FindAsync(id);

        if (detalle != null)
        {
            int pedidoId = detalle.PedidoId;

            _context.DetallesPedido.Remove(detalle);
            await _context.SaveChangesAsync();

            bool quedanLineas = _context.DetallesPedido
                .Any(d => d.PedidoId == pedidoId);

            if (!quedanLineas)
            {
                var pedido = await _context.Pedidos.FindAsync(pedidoId);

                if (pedido != null)
                {
                    _context.Pedidos.Remove(pedido);
                    await _context.SaveChangesAsync();
                }

                HttpContext.Session.Remove("NumPedido");
            }
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> MasCantidad(int id)
    {
        var detalle = await _context.DetallesPedido.FindAsync(id);

        if (detalle != null)
        {
            detalle.Cantidad++;

            _context.Update(detalle);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> MenosCantidad(int id)
    {
        var detalle = await _context.DetallesPedido.FindAsync(id);

        if (detalle != null && detalle.Cantidad > 1)
        {
            detalle.Cantidad--;

            _context.Update(detalle);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> ConfirmarPedido(int id)
    {
        var pedido = await _context.Pedidos
            .Include(p => p.Detalles)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pedido == null || !pedido.Detalles.Any())
        {
            return RedirectToAction("CarritoVacio");
        }
        if (pedido.EstadoId == 2)
        {
            TempData["Mensaje"] = "Pedido ya confirmado.";
            return RedirectToAction("Index", "Escaparate");
        }
        pedido.EstadoId = 2;
        pedido.Confirmado = DateTime.Now;

        _context.Update(pedido);
        await _context.SaveChangesAsync();

        HttpContext.Session.Remove("NumPedido");

        return RedirectToAction("Index", "Escaparate");
    }

}