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
    public class EnsemblesController : Controller
    {
        private readonly AppDbContext _context;

        public EnsemblesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Ensembles
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Ensembles.Include(e => e.TypeEnsemble);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Ensembles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ensembles == null)
            {
                return NotFound();
            }

            var ensemble = await _context.Ensembles
                .Include(e => e.TypeEnsemble)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ensemble == null)
            {
                return NotFound();
            }

            return View(ensemble);
        }

        // GET: Ensembles/Create
        public IActionResult Create()
        {
            ViewData["TypeEnsembleId"] = new SelectList(_context.TypeEnsembles, "Id", "Name");
            return View();
        }

        // POST: Ensembles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,TypeEnsembleId")] Ensemble ensemble)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ensemble);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeEnsembleId"] = new SelectList(_context.TypeEnsembles, "Id", "Name", ensemble.TypeEnsembleId);
            return View(ensemble);
        }

        // GET: Ensembles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ensembles == null)
            {
                return NotFound();
            }

            var ensemble = await _context.Ensembles.FindAsync(id);
            if (ensemble == null)
            {
                return NotFound();
            }
            ViewData["TypeEnsembleId"] = new SelectList(_context.TypeEnsembles, "Id", "Name", ensemble.TypeEnsembleId);
            return View(ensemble);
        }

        // POST: Ensembles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TypeEnsembleId")] Ensemble ensemble)
        {
            if (id != ensemble.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ensemble);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnsembleExists(ensemble.Id))
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
            ViewData["TypeEnsembleId"] = new SelectList(_context.TypeEnsembles, "Id", "Name", ensemble.TypeEnsembleId);
            return View(ensemble);
        }

        // GET: Ensembles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ensembles == null)
            {
                return NotFound();
            }

            var ensemble = await _context.Ensembles
                .Include(e => e.TypeEnsemble)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ensemble == null)
            {
                return NotFound();
            }

            return View(ensemble);
        }

        // POST: Ensembles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ensembles == null)
            {
                return Problem("Entity set 'AppDbContext.Ensembles'  is null.");
            }
            var ensemble = await _context.Ensembles.FindAsync(id);
            if (ensemble != null)
            {
                _context.Ensembles.Remove(ensemble);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnsembleExists(int id)
        {
          return _context.Ensembles.Any(e => e.Id == id);
        }
    }
}
