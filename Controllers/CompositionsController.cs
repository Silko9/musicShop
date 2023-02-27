﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using musicShop.Models;

namespace musicShop.Controllers
{
    public class CompositionsController : Controller
    {
        private readonly AppDbContext _context;

        public CompositionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Compositions
        public async Task<IActionResult> Index()
        {
              return View(await _context.Compositions.ToListAsync());
        }

        // GET: Compositions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Compositions == null)
            {
                return NotFound();
            }

            var composition = await _context.Compositions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (composition == null)
            {
                return NotFound();
            }

            return View(composition);
        }

        // GET: Compositions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Compositions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NameAuthor,SurnameAuthor,PatronymicAuthor,DateCreate")] Composition composition)
        {
            if (ModelState.IsValid)
            {
                _context.Add(composition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(composition);
        }

        // GET: Compositions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Compositions == null)
            {
                return NotFound();
            }

            var composition = await _context.Compositions.FindAsync(id);
            if (composition == null)
            {
                return NotFound();
            }
            return View(composition);
        }

        // POST: Compositions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,NameAuthor,SurnameAuthor,PatronymicAuthor,DateCreate")] Composition composition)
        {
            if (id != composition.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(composition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompositionExists(composition.Id))
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
            return View(composition);
        }

        // GET: Compositions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Compositions == null)
            {
                return NotFound();
            }

            var composition = await _context.Compositions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (composition == null)
            {
                return NotFound();
            }

            return View(composition);
        }

        // POST: Compositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Compositions == null)
            {
                return Problem("Entity set 'AppDbContext.Compositions'  is null.");
            }
            var composition = await _context.Compositions.FindAsync(id);
            if (composition != null)
            {
                _context.Compositions.Remove(composition);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompositionExists(int id)
        {
          return _context.Compositions.Any(e => e.Id == id);
        }
    }
}
