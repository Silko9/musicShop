﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using musicShop.Models;
using musicShop.Models.ViewModels;

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
            var appDbContext = _context.Ensembles.Include(e => e.TypeEnsemble).Include(e => e.Musicians);
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
                .Include(e => e.TypeEnsemble.Name)
                .Include(e => e.Musicians)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ensemble == null)
            {
                return NotFound();
            }

            EnsembleDetailsViewModel viewModel = new EnsembleDetailsViewModel();
            viewModel.Ensemble = ensemble;

            List<Musician> musicians = new List<Musician>();
            foreach (var musician in ensemble.Musicians)
                musicians.Add(await _context.Musicians.FindAsync(musician.Id));
            viewModel.Musicians = musicians;

            return View(viewModel);
        }

        public async Task<IActionResult> AddMusicianToEnsemble(int id)
        {
            ViewBag.EnsembleId = id;
            Ensemble ensemble = await _context.Ensembles.Include(m => m.Musicians)
                .Include(m => m.TypeEnsemble)
                .FirstOrDefaultAsync(m => m.Id == id);
            List<Musician> musicians = _context.Musicians.ToList();
            foreach (var musician in ensemble.Musicians)
                musicians.Remove(musician);
            return View(musicians);
        }


        [HttpPost]
        public async Task<IActionResult> AddMusicianToEnsemble(int ensembleId, int musicianId)
        {

            Ensemble ensemble = _context.Ensembles.Include(sp => sp.Musicians).Include(sp => sp.TypeEnsemble).FirstOrDefault(sp => sp.Id == ensembleId);
            Musician musician = _context.Musicians.Include(d => d.Roles).Include(d => d.Ensembles).FirstOrDefault(d => d.Id == musicianId);

            ensemble.Musicians.Add(musician);
            musician.Ensembles.Add(ensemble);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = ensembleId });
        }

        // GET: Ensembles/Create
        public IActionResult Create(int? typeEnsembleId)
        {
            ViewBag.TypeEnsembleName = _context.TypeEnsembles.Find(typeEnsembleId);
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
                _context.Ensembles.Add(ensemble);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            } 

            return View(ensemble);
        }

        public IActionResult SelectTypeEnsemble(int? id, bool? fromCreate)
        {
            ViewBag.id = id;
            ViewBag.fromCreate = fromCreate;
            var types = _context.TypeEnsembles.ToList();
            return View(types);
        }

        [HttpPost]
        public async Task<IActionResult> SelectTypeEnsemble(int typeEnsembleId)
        {
            return View();
        }

        // GET: Ensembles/Edit/5
        public async Task<IActionResult> Edit(int? id, int? typeEnsembleId)
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
            ViewBag.id = id;
            ViewBag.TypeEnsembleName = _context.TypeEnsembles.Find(typeEnsembleId);
            //ViewData["TypeEnsembleId"] = new SelectList(_context.TypeEnsembles, "Id", "Name", ensemble.TypeEnsembleId);
            //удалить если через время ни чего не поломается
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
          return (_context.Ensembles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
