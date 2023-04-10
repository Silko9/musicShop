using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using musicShop.Models;

namespace musicShop.Controllers
{
    [Authorize(Roles = "cashier, admin")]
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
        public async Task<IActionResult> Details(int? id, string? from, int? fromId)
        {
            if (id == null || _context.Loggings == null)
            {
                return NotFound();
            }
            ViewBag.From = from;
            ViewBag.Id = fromId;
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
        public IActionResult Create(int? id, string? from, int? recordId)
        {
            ViewBag.From = from;
            ViewBag.Id = id;
            ViewBag.Record = _context.Records.Find(recordId);
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
                
                if (from == "Orders")
                    logging.TypeLoggingId = Const.ORDER_ID;
                if (from == "Deliveries")
                    logging.TypeLoggingId = Const.DELIVERY_ID;
                if(idItem != null)
                    logging.Operation = (int)idItem;
                _context.Add(logging);
                await _context.SaveChangesAsync();
                if (from != null)
                    return Redirect("~/" + from + "/Details/" + idItem);
                return RedirectToAction(nameof(Index));
            }
            return View(logging);
        }

        public IActionResult SelectRecord(int? id, bool? fromCreate, string? from)
        {
            ViewBag.id = id;
            ViewBag.fromCreate = fromCreate;
            ViewBag.From = from;
            var record = _context.Records.
                Include(x => x.Composition).
                ToList();
            return View(record);
        }

        [HttpPost]
        public async Task<IActionResult> SelectTypeEnsemble()
        {
            return View();
        }

        // GET: Loggings/Edit/5
        public async Task<IActionResult> Edit(int? id, string? from, int? recordId)
        {
            if (id == null || _context.Loggings == null)
            {
                return NotFound();
            }

            var logging = await _context.Loggings.
                Include(p => p.Record).
                FirstOrDefaultAsync(p => p.Id == id);
            ViewBag.Id = id;
            ViewBag.From = from;
            ViewBag.Record = _context.Records.Find(recordId);
            if (logging == null)
            {
                return NotFound();
            }
            return View(logging);
        }

        // POST: Loggings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RecordId,Amount,TypeLoggingId,Operation")] Logging logging, string? from)
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
                if (from != null)
                    return Redirect("~/" + from + "/Details/" + logging.Operation);
                return RedirectToAction(nameof(Index));
            }
            return await ReturnRedirect(logging);
        }

        // GET: Loggings/Delete/5
        public async Task<IActionResult> Delete(int? id, string? from, int? fromId)
        {
            if (id == null || _context.Loggings == null)
            {
                return NotFound();
            }
            ViewBag.Id = fromId;
            ViewBag.From = from;
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
            return await ReturnRedirect(logging);

        }

        private bool LoggingExists(int id)
        {
          return _context.Loggings.Any(e => e.Id == id);
        }

        private async Task<IActionResult> ReturnRedirect(Logging logging)
        {
            if (logging.TypeLoggingId == Const.ORDER_ID)
                return Redirect("~/" + "Orders/Details/" + logging.Operation);
            else
                return Redirect("~/" + "Deliveries/Details/" + logging.Operation);
        }
    }
}
