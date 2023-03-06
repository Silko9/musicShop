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

        [HttpPost]
        public async Task<IActionResult> ChooseAddress(int clientId, string address)
        {
            Client client = await _context.Clients.FindAsync(clientId);
            ViewBag.ClientId = clientId;
            ViewBag.Address = address;
            ViewBag.Client = $"{client.Name} {client.Surname} {client.Patronymic}";
            var appDbContext = _context.Records.Include(p => p.Composition);
            return View("ChooseRecords", await appDbContext.ToListAsync());
            //return View("ChooseRecords", await _context.Records.ToListAsync());
        }

        public IActionResult selectedRecords(int[] selectedNumbers)
        {
            // обработка выбранных пластинок
            return View("Index", "Client");
        }
    }
}
