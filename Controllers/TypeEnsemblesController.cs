using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using musicShop.Models;
using musicShop.Models.ViewModal;
using musicShop.Models.ViewModels;

namespace musicShop.Controllers
{
    public class TypeEnsemblesController : Controller
    {
        private readonly AppDbContext _context;

        public TypeEnsemblesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TypeEnsembles
        public async Task<IActionResult> Index()
        {
              return View(await _context.TypeEnsembles.ToListAsync());
        }

        // GET: TypeEnsembles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TypeEnsembles == null)
            {
                return NotFound();
            }

            var typeEnsemble = await _context.TypeEnsembles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeEnsemble == null)
            {
                return NotFound();
            }
            TypeEnsembleDetailsViewModel viewModel = new TypeEnsembleDetailsViewModel();
            viewModel.TypeEnsemble = typeEnsemble;
            viewModel.Ensembles = _context.Ensembles.Where(p => p.TypeEnsembleId == typeEnsemble.Id);

            return View(viewModel);
        }

        public async Task<IActionResult> AddEnsembleToTypeEnsemble(int id)
        {
            ViewBag.TypeEnsembleId = id;
            TypeEnsemble typeEnsemble = await _context.TypeEnsembles.FindAsync(id);
            return View(_context.Ensembles.Where(p => p.TypeEnsembleId != typeEnsemble.Id).ToList());
        }


        [HttpPost]
        public async Task<IActionResult> AddEnsembleToTypeEnsemble(int ensembleId, int typeEnsembleId)
        {
            Ensemble ensemble = _context.Ensembles.Find(ensembleId);
            ensemble.TypeEnsembleId = typeEnsembleId;
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = typeEnsembleId });
        }

        // GET: TypeEnsembles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeEnsembles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] TypeEnsemble typeEnsemble)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeEnsemble);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeEnsemble);
        }

        // GET: TypeEnsembles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TypeEnsembles == null)
            {
                return NotFound();
            }

            var typeEnsemble = await _context.TypeEnsembles.FindAsync(id);
            if (typeEnsemble == null)
            {
                return NotFound();
            }
            return View(typeEnsemble);
        }

        // POST: TypeEnsembles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] TypeEnsemble typeEnsemble)
        {
            if (id != typeEnsemble.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeEnsemble);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeEnsembleExists(typeEnsemble.Id))
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
            return View(typeEnsemble);
        }

        // GET: TypeEnsembles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TypeEnsembles == null)
            {
                return NotFound();
            }

            var typeEnsemble = await _context.TypeEnsembles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeEnsemble == null)
            {
                return NotFound();
            }

            return View(typeEnsemble);
        }

        // POST: TypeEnsembles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TypeEnsembles == null)
            {
                return Problem("Entity set 'AppDbContext.TypeEnsembles'  is null.");
            }
            var typeEnsemble = await _context.TypeEnsembles.FindAsync(id);
            if (typeEnsemble != null)
            {
                _context.TypeEnsembles.Remove(typeEnsemble);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeEnsembleExists(int id)
        {
          return _context.TypeEnsembles.Any(e => e.Id == id);
        }
    }
}
