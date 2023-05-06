using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using musicShop.Models;
using musicShop.Models.ViewModels;

namespace musicShop.Controllers
{
    public class RecordsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public RecordsController(AppDbContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: Records
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Records
                .Include(p => p.Performances)
                .Include(p => p.Composition);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Records/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var contexPerformances = _context.Performances.Include(p => p.Ensemble);
            if (id == null)
            {
                return NotFound();
            }

            var @record = await _context.Records
                .Include(p => p.Performances)
                .Include(p => p.Composition)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (@record == null)
            {
                return NotFound();
            }

            // Load the Composition entity explicitly if it was not loaded with the Record
            if (@record.Composition == null)
            {
                @record.Composition = await _context.Compositions.FirstOrDefaultAsync(c => c.Id == @record.CompositionId);
            }

            RecordDetailsViewModel viewModel = new RecordDetailsViewModel();
            viewModel.Record = record;

            List<Performance> performances = new List<Performance>();
            foreach (var performance in record.Performances)
                performances.Add(contexPerformances.First(p => p.Id == performance.Id));
            viewModel.Performances = performances;

            if (!string.IsNullOrEmpty(record.phote))
            {
                byte[] photodata =
                System.IO.File.ReadAllBytes(_appEnvironment.WebRootPath + record.phote);
                ViewBag.Photodata = photodata;
            }
            else
                ViewBag.Photodata = null;

            return View(viewModel);
        }

        [Authorize(Roles = "cashier, admin")]
        public async Task<IActionResult> AddPerformanceToRecord(int id)
        {
            ViewBag.RecordId = id;
            Record record = await _context.Records
                .Include(p => p.Performances)
                .Include(p => p.Composition)
                .FirstOrDefaultAsync(m => m.Id == id);
            List<Performance> performances = _context.Performances
                .Include(p => p.Composition)
                .Include(m => m.Records)
                .Include(sp => sp.Ensemble)
                .Where(p => p.CompositionId == record.CompositionId)
                .ToList();
            foreach (var performance in record.Performances)
                performances.Remove(performance);
            return View(performances);
        }


        [HttpPost]
        [Authorize(Roles = "cashier, admin")]
        public async Task<IActionResult> AddPerformanceToRecord(int recordId, int performanceId)
        {

            Performance performance = _context.Performances.Include(sp => sp.Records).Include(sp => sp.Ensemble).Include(sp => sp.Composition).FirstOrDefault(sp => sp.Id == performanceId);
            Record record = _context.Records.Include(d => d.Performances).Include(d => d.Composition).FirstOrDefault(d => d.Id == recordId);

            performance.Records.Add(record);
            record.Performances.Add(performance);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = recordId });
        }

        [Authorize(Roles = "cashier, admin")]
        public async Task<IActionResult> DeleteRelationPerformance(int recordId, int performanceId)
        {
            Record record = _context.Records.Include(p => p.Performances).First(p => p.Id == recordId);
            Performance performance = _context.Performances.Include(p => p.Records).First(s => s.Id == performanceId);

            record.Performances.Remove(performance);
            performance.Records.Remove(record);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = recordId });
        }

        // GET: Records/Create
        [Authorize(Roles = "cashier, admin")]
        public IActionResult Create(int? compositionId)
        {
            ViewBag.Composition = _context.Compositions.Find(compositionId);
            return View();
        }

        // POST: Records/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "cashier, admin")]
        public async Task<IActionResult> Create([Bind("Id,Number,RetailPrice,WholesalePrice,CompositionId,Amount")] Record @record, IFormFile? upload)
        {
            /*if (ModelState.IsValid)
            {*/
                _context.Add(@record);
                await _context.SaveChangesAsync();
                if (upload != null)
                {
                    string path = "/Files/record" + record.Id;
                    using (var fileStream = new
                    FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await upload.CopyToAsync(fileStream);
                    }
                    record.phote = path;
                }
                _context.Update(@record);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            return View(@record);
        }

        [Authorize(Roles = "cashier, admin")]
        public IActionResult SelectComposition(int? id, bool? fromCreate)
        {
            ViewBag.id = id;
            ViewBag.fromCreate = fromCreate;
            var composition = _context.Compositions.ToList();
            return View(composition);
        }

        [HttpPost]
        [Authorize(Roles = "cashier, admin")]
        public async Task<IActionResult> SelectComposition(int compositionId)
        {
            return View();
        }

        // GET: Records/Edit/5
        [Authorize(Roles = "cashier, admin")]
        public async Task<IActionResult> Edit(int? id, int? compositionId)
        {
            if (id == null || _context.Records == null)
            {
                return NotFound();
            }

            var record = await _context.Records
                .Include(p => p.Composition)
                .Include(p => p.Performances)
                .FirstOrDefaultAsync(d => d.Id == id);
            if (record == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(record.phote))
            {
                byte[] photodata =
                System.IO.File.ReadAllBytes(_appEnvironment.WebRootPath + record.phote);
                ViewBag.Photodata = photodata;
            }
            else
                ViewBag.Photodata = null;
            ViewBag.id = id;
            ViewBag.Composition = _context.Compositions.Find(compositionId);
            return View(record);
        }

        // POST: Records/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "cashier, admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,RetailPrice,WholesalePrice,CompositionId,Amount")] Record @record, IFormFile? upload, string? Photo)
        {
            if (id != @record.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (upload != null)
                {
                    string path = "/Files/record" + id;
                    using (var fileStream = new
                    FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await upload.CopyToAsync(fileStream);
                    }
                    if (!string.IsNullOrEmpty(record.phote))
                    {
                        System.IO.File.Delete(_appEnvironment.WebRootPath +
                        record.phote);
                    }
                    record.phote = path;
                }
                else
                    record.phote = Photo;
                try
                {
                    _context.Update(@record);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecordExists(@record.Id))
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
            return View(@record);
        }

        // GET: Records/Delete/5
        [Authorize(Roles = "cashier, admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Records == null)
            {
                return NotFound();
            }

            var @record = await _context.Records
                .Include(p => p.Composition)
                .Include(p => p.Performances)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@record == null)
            {
                return NotFound();
            }

            return View(@record);
        }

        // POST: Records/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "cashier, admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Records == null)
            {
                return Problem("Entity set 'AppDbContext.Records'  is null.");
            }
            var @record = await _context.Records.FindAsync(id);
            if (@record != null)
            {
                _context.Records.Remove(@record);
                if (!string.IsNullOrEmpty(record.phote))
                    System.IO.File.Delete(_appEnvironment.WebRootPath + record.phote);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecordExists(int id)
        {
          return _context.Records.Any(e => e.Id == id);
        }
    }
}
