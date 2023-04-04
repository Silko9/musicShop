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
    public class TypeLoggingsController : Controller
    {
        private readonly AppDbContext _context;

        public TypeLoggingsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TypeLoggings
        public async Task<IActionResult> Index()
        {
              return _context.TypeLogging != null ? 
                          View(await _context.TypeLogging.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.TypeLogging'  is null.");
        }

        // GET: TypeLoggings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TypeLogging == null)
            {
                return NotFound();
            }

            var typeLogging = await _context.TypeLogging
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeLogging == null)
            {
                return NotFound();
            }

            return View(typeLogging);
        }

        // GET: TypeLoggings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeLoggings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] TypeLogging typeLogging)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeLogging);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeLogging);
        }

        // GET: TypeLoggings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TypeLogging == null)
            {
                return NotFound();
            }

            var typeLogging = await _context.TypeLogging.FindAsync(id);
            if (typeLogging == null)
            {
                return NotFound();
            }
            return View(typeLogging);
        }

        // POST: TypeLoggings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] TypeLogging typeLogging)
        {
            if (id != typeLogging.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeLogging);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeLoggingExists(typeLogging.Id))
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
            return View(typeLogging);
        }

        // GET: TypeLoggings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TypeLogging == null)
            {
                return NotFound();
            }

            var typeLogging = await _context.TypeLogging
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeLogging == null)
            {
                return NotFound();
            }

            return View(typeLogging);
        }

        // POST: TypeLoggings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TypeLogging == null)
            {
                return Problem("Entity set 'AppDbContext.TypeLogging'  is null.");
            }
            var typeLogging = await _context.TypeLogging.FindAsync(id);
            if (typeLogging != null)
            {
                _context.TypeLogging.Remove(typeLogging);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeLoggingExists(int id)
        {
          return (_context.TypeLogging?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
