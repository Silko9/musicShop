using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using musicShop.Models;

namespace musicShop.Controllers
{
    public class CreateOrderController : Controller
    {
        private readonly AppDbContext _context;

        public CreateOrderController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Clients.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(int clientId)
        {
            Client client = await _context.Clients.FindAsync(clientId);
            if (client == null)
                return NotFound();
            ViewBag.ClientId = clientId;
            ViewBag.Client = client;
            return View("ChooseAddress");
        }
    }
}
