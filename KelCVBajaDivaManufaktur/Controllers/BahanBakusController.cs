using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KelCVBajaDivaManufaktur.Data;
using KelCVBajaDivaManufaktur.Models;

namespace KelCVBajaDivaManufaktur.Controllers
{
    public class BahanBakusController : Controller
    {
        private readonly KelCVBajaDivaManufakturContext _context;

        public BahanBakusController(KelCVBajaDivaManufakturContext context)
        {
            _context = context;
        }

        // GET: BahanBakus
        public async Task<IActionResult> Index(string searchString)
        {
            if (_context.BahanBaku == null)
            {
                return Problem("Entity set 'KelCVBajaDivaManufakturContext.BahanBaku'  is null.");
            }

            var bahanbakus = from m in _context.BahanBaku
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                bahanbakus = bahanbakus.Where(s => s.NamaBahan!.Contains(searchString));
            }

            return View(await bahanbakus.ToListAsync());
        }

        // GET: BahanBakus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BahanBaku == null)
            {
                return NotFound();
            }

            var bahanBaku = await _context.BahanBaku
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bahanBaku == null)
            {
                return NotFound();
            }

            return View(bahanBaku);
        }

        // GET: BahanBakus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BahanBakus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NamaBahan,Stok,Satuan,Supplier")] BahanBaku bahanBaku)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bahanBaku);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bahanBaku);
        }

        // GET: BahanBakus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BahanBaku == null)
            {
                return NotFound();
            }

            var bahanBaku = await _context.BahanBaku.FindAsync(id);
            if (bahanBaku == null)
            {
                return NotFound();
            }
            return View(bahanBaku);
        }

        // POST: BahanBakus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NamaBahan,Stok,Satuan,Supplier")] BahanBaku bahanBaku)
        {
            if (id != bahanBaku.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bahanBaku);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BahanBakuExists(bahanBaku.Id))
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
            return View(bahanBaku);
        }

        // GET: BahanBakus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BahanBaku == null)
            {
                return NotFound();
            }

            var bahanBaku = await _context.BahanBaku
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bahanBaku == null)
            {
                return NotFound();
            }

            return View(bahanBaku);
        }

        // POST: BahanBakus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BahanBaku == null)
            {
                return Problem("Entity set 'KelCVBajaDivaManufakturContext.BahanBaku'  is null.");
            }
            var bahanBaku = await _context.BahanBaku.FindAsync(id);
            if (bahanBaku != null)
            {
                _context.BahanBaku.Remove(bahanBaku);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BahanBakuExists(int id)
        {
          return (_context.BahanBaku?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
