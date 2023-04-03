using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using musicShop.Models;

namespace musicShop.Controllers
{
    public class CreateDeliveryController : Controller
    {
        private readonly AppDbContext _context;

        public CreateDeliveryController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Providers.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(int providerId)
        {
            Provider provider = await _context.Providers.FindAsync(providerId);
            if (provider == null)
                return NotFound();
            ViewBag.ProviderId = providerId;
            ViewBag.Provider = provider;
            return View("ChooseDate");
        }

        public async Task<IActionResult> ChooseDate(int providerId)
        {
            Provider provider = await _context.Providers.FindAsync(providerId);
            if (provider == null)
                return NotFound();
            ViewBag.ProviderId = providerId;
            ViewBag.Provider = provider;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChooseDate(int providerId, string date)
        {
            Provider provider = await _context.Providers.FindAsync(providerId);
            ViewBag.ProviderId = providerId;
            ViewBag.Date = date;
            ViewBag.Provider = provider.Name;
            var appDbContext = _context.Records.Include(p => p.Composition);
            return View("ChooseRecords", await appDbContext.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> ChooseRecords(List<RecordSelection> records, int providerId, string date)
        {
            var selectedRecords = records.Where(r => r.Count > 0).ToList();
            ICollection<Logging> loggings = new List<Logging>();

            DateTime dateDelivery = DateTime.ParseExact(date, "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);

            Delivery delivery = new Delivery
            {
                Loggings = loggings,
                Provider = await _context.Providers.FindAsync(providerId),
                ProviderId = providerId,
                DateDelivery = dateDelivery,
                DateCreate = DateTime.Today.Date
            };
            _context.Deliveries.Add(delivery);
            _context.SaveChanges();

            foreach (var record in selectedRecords)
            {
                Logging logging = new Logging
                {
                    Record = await _context.Records.FindAsync(record.Id),
                    Amount = record.Count,
                    Delivery = await _context.Deliveries.FindAsync(delivery.Id)
                };
                _context.Loggings.Add(logging);
                _context.SaveChanges();
            }

            return View("ViewDelivery", delivery);
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
