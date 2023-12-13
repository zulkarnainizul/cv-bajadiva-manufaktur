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
    public class PenggunasController : Controller
    {
        private readonly KelCVBajaDivaManufakturContext _context;

        public PenggunasController(KelCVBajaDivaManufakturContext context)
        {
            _context = context;
        }

        // GET: Penggunas
        public async Task<IActionResult> Index(string searchString)
        {
            if (_context.Pengguna == null)
            {
                return Problem("\"Entity set 'KelCVBajaDivaManufakturContext.Pengguna'  is null.");
            }

            var penggunas = from m in _context.Pengguna
                             select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                penggunas = penggunas.Where(s => s.NamaPengguna!.Contains(searchString));
            }

            return View(await penggunas.ToListAsync());
        }

        // GET: Penggunas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pengguna == null)
            {
                return NotFound();
            }

            var pengguna = await _context.Pengguna
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pengguna == null)
            {
                return NotFound();
            }

            return View(pengguna);
        }

        // GET: Penggunas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Penggunas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NamaPengguna,Username,Password,HakAkses")] Pengguna pengguna)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pengguna);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pengguna);
        }

        // GET: Penggunas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pengguna == null)
            {
                return NotFound();
            }

            var pengguna = await _context.Pengguna.FindAsync(id);
            if (pengguna == null)
            {
                return NotFound();
            }
            return View(pengguna);
        }

        // POST: Penggunas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NamaPengguna,Username,Password,HakAkses")] Pengguna pengguna)
        {
            if (id != pengguna.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pengguna);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PenggunaExists(pengguna.Id))
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
            return View(pengguna);
        }

        // GET: Penggunas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pengguna == null)
            {
                return NotFound();
            }

            var pengguna = await _context.Pengguna
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pengguna == null)
            {
                return NotFound();
            }

            return View(pengguna);
        }

        // POST: Penggunas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pengguna == null)
            {
                return Problem("Entity set 'KelCVBajaDivaManufakturContext.Pengguna'  is null.");
            }
            var pengguna = await _context.Pengguna.FindAsync(id);
            if (pengguna != null)
            {
                _context.Pengguna.Remove(pengguna);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PenggunaExists(int id)
        {
          return (_context.Pengguna?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
