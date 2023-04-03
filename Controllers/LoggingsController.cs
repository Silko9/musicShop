using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using musicShop.Models;

namespace musicShop.Controllers
{
    public class LoggingsController : Controller
    {
        private readonly AppDbContext _context;

        public LoggingsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Loggings
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Loggings.Include(l => l.Record);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Loggings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Loggings == null)
            {
                return NotFound();
            }

            var logging = await _context.Loggings
                .Include(l => l.Record)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logging == null)
            {
                return NotFound();
            }

            return View(logging);
        }

        // GET: Loggings/Create
        public IActionResult Create(int? id, string? from)
        {
            ViewData["RecordId"] = new SelectList(_context.Records, "Id", "Number");
            ViewBag.From = from;
            ViewBag.Id = id;
            return View();
        }

        // POST: Loggings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecordId,Amount")] Logging logging, int? idItem, string? from)
        {
            if (ModelState.IsValid)
            {
                _context.Add(logging);
                await _context.SaveChangesAsync();
                if (from == "Orders")
                    logging.OrderId = idItem;
                if (from == "Deliveries")
                    logging.DeliveryId = idItem;
                if (from != null)
                {
                    _context.Update(logging);
                    await _context.SaveChangesAsync();
                    return Redirect("~/" + from + "/Details/" + idItem);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecordId"] = new SelectList(_context.Records, "Id", "Number", logging.RecordId);
            return View(logging);
        }

        // GET: Loggings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Loggings == null)
            {
                return NotFound();
            }

            var logging = await _context.Loggings.FindAsync(id);
            if (logging == null)
            {
                return NotFound();
            }
            ViewData["RecordId"] = new SelectList(_context.Records, "Id", "Number", logging.RecordId);
            return View(logging);
        }

        // POST: Loggings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RecordId,Amount")] Logging logging)
        {
            if (id != logging.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(logging);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoggingExists(logging.Id))
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
            ViewData["RecordId"] = new SelectList(_context.Records, "Id", "Number", logging.RecordId);
            return View(logging);
        }

        // GET: Loggings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Loggings == null)
            {
                return NotFound();
            }

            var logging = await _context.Loggings
                .Include(l => l.Record)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logging == null)
            {
                return NotFound();
            }

            return View(logging);
        }

        // POST: Loggings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Loggings == null)
            {
                return Problem("Entity set 'AppDbContext.Loggings'  is null.");
            }
            var logging = await _context.Loggings.FindAsync(id);
            if (logging != null)
            {
                _context.Loggings.Remove(logging);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoggingExists(int id)
        {
          return _context.Loggings.Any(e => e.Id == id);
        }
    }
}
