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

        public async Task<IActionResult> ChooseAddress(int id)
        {
            Client client = await _context.Clients.FindAsync(id);
            if (client == null)
                return NotFound();
            ViewBag.ClientId = id;
            ViewBag.Client = client;
            return View("ChooseAddress");
        }

        [HttpPost]
        public async Task<IActionResult> ChooseAddress(int clientId, string address, string date)
        {
            Client client = await _context.Clients.FindAsync(clientId);
            ViewBag.ClientId = clientId;
            ViewBag.Address = address;
            ViewBag.Date = date;
            ViewBag.Client = $"{client.Name} {client.Surname} {client.Patronymic}";
            var appDbContext = _context.Records.Include(p => p.Composition);
            return View("ChooseRecords", await appDbContext.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> ChooseRecords(List<RecordSelection> records, int clientId, string address, string date)
        {
            var selectedRecords = records.Where(r => r.Count > 0).ToList();
            ICollection<Logging> loggings = new List<Logging>();

            DateTime dateDelivery = DateTime.ParseExact(date, "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);

            Order order = new Order
            {
                Loggings = loggings,
                Client = await _context.Clients.FindAsync(clientId),
                ClientId = clientId,
                Address = address,
                DateDelivery = dateDelivery,
                DateCreate = DateTime.Today.Date
            };
            _context.Orders.Add(order);
            _context.SaveChanges();

            foreach (var record in selectedRecords)
            {
                Logging logging = new Logging
                {
                    Record = await _context.Records.FindAsync(record.Id),
                    Amount = record.Count,
                    Order = await _context.Orders.FindAsync(order.Id)
                };
                _context.Loggings.Add(logging);
                _context.SaveChanges();
            }

            return View("ViewOrder", order);
        }


        public class RecordSelection
        {
            public int Id { get; set; }
            public string Number { get; set; }
            public decimal RetailPrice { get; set; }
            public string CompositionName { get; set; }
            public int Count { get; set; }
        }
    }
}
