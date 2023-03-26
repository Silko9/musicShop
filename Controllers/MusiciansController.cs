using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using musicShop.Models;
using musicShop.Models.ViewModal;

namespace musicShop.Controllers
{
    public class MusiciansController : Controller
    {
        private readonly AppDbContext _context;

        public MusiciansController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Musicians
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Musicians.Include(e => e.Roles).Include(e => e.Ensembles);
            /*var appDbContext2 = _context.Ensembles.Include(e => e.TypeEnsemble);
            var combinedDbContext = appDbContext.Concat(appDbContext2);*/

            return View(await appDbContext.ToListAsync());
        }

        // GET: Musicians/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Musicians == null)
            {
                return NotFound();
            }

            var musician = await _context.Musicians
                .Include(m => m.Roles)
                .Include(m => m.Ensembles)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musician == null)
            {
                return NotFound();
            }

            MusicianDetailsViewModel viewModel = new MusicianDetailsViewModel();
            viewModel.Musician = musician;

            List<Role> roles = new List<Role>();
            foreach (var role in musician.Roles)
                roles.Add(await _context.Roles.FindAsync(role.Id));
            List<Ensemble> ensembles = new List<Ensemble>();
            foreach (var ensemble in musician.Ensembles)
                ensembles.Add(ensemble);
            viewModel.Roles = roles;
            viewModel.Ensembles = ensembles;

            return View(viewModel);
        }

        public async Task<IActionResult> AddRoleToMusician(int id)
        {
            ViewBag.MusicianId = id;
            Musician musician = await _context.Musicians.Include(m => m.Roles)
                .FirstOrDefaultAsync(m => m.Id == id);
            List<Role> roles = _context.Roles.ToList();
            foreach (var role in musician.Roles)
                roles.Remove(role);
            return View(roles);
        }


        [HttpPost]
        public async Task<IActionResult> AddRoleToMusician(int musicianId, int roleId)
        {

            Role role = _context.Roles.Include(sp => sp.Musicians).FirstOrDefault(sp => sp.Id == roleId);
            Musician musician = _context.Musicians.Include(d => d.Roles).FirstOrDefault(d => d.Id == musicianId);

            musician.Roles.Add(role);
            role.Musicians.Add(musician);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = musicianId });
        }

        public async Task<IActionResult> AddEnsembleToMusician(int id)
        {
            ViewBag.MusicianId = id;
            Musician musician = await _context.Musicians.Include(m => m.Ensembles)
                .FirstOrDefaultAsync(m => m.Id == id);
            List<Ensemble> ensembles = _context.Ensembles.Include(s => s.TypeEnsemble).ToList();
            foreach (var ensemble in musician.Ensembles)
                ensembles.Remove(ensemble);
            return View(ensembles);
        }


        [HttpPost]
        public async Task<IActionResult> AddEnsembleToMusician(int musicianId, int ensembleId)
        {

            Ensemble ensemble = _context.Ensembles.Include(sp => sp.Musicians).Include(s => s.TypeEnsemble).FirstOrDefault(sp => sp.Id == ensembleId);
            Musician musician = _context.Musicians.Include(d => d.Ensembles).FirstOrDefault(d => d.Id == musicianId);

            musician.Ensembles.Add(ensemble);
            ensemble.Musicians.Add(musician);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = musicianId });
        }

        // GET: Musicians/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Musicians/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Patronymic,PhotePath")] Musician musician)
        {
            if (ModelState.IsValid)
            {
                _context.Add(musician);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(musician);
        }

        // GET: Musicians/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Musicians == null)
            {
                return NotFound();
            }

            var musician = await _context.Musicians.FindAsync(id);
            if (musician == null)
            {
                return NotFound();
            }
            return View(musician);
        }

        // POST: Musicians/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Patronymic,PhotePath")] Musician musician)
        {
            if (id != musician.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(musician);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusicianExists(musician.Id))
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
            return View(musician);
        }

        // GET: Musicians/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Musicians == null)
            {
                return NotFound();
            }

            var musician = await _context.Musicians
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musician == null)
            {
                return NotFound();
            }

            return View(musician);
        }

        // POST: Musicians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Musicians == null)
            {
                return Problem("Entity set 'AppDbContext.Musicians'  is null.");
            }
            var musician = await _context.Musicians.FindAsync(id);
            if (musician != null)
            {
                _context.Musicians.Remove(musician);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MusicianExists(int id)
        {
          return _context.Musicians.Any(e => e.Id == id);
        }
    }
}
