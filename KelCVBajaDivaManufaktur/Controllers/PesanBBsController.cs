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
    public class PesanBBsController : Controller
    {
        private readonly KelCVBajaDivaManufakturContext _context;

        public PesanBBsController(KelCVBajaDivaManufakturContext context)
        {
            _context = context;
        }

        // GET: PesanBBs
        public async Task<IActionResult> Index()
        {
            var kelCVBajaDivaManufakturContext = _context.PesanBB
                .Include(p => p.Supplier)
                .Include(p => p.BahanBaku);
            return View(await kelCVBajaDivaManufakturContext.ToListAsync());
        }

        // GET: PesanBBs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PesanBB == null)
            {
                return NotFound();
            }

            var pesanBB = await _context.PesanBB
                .Include(p => p.Supplier)
                .Include(p => p.BahanBaku)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pesanBB == null)
            {
                return NotFound();
            }

            return View(pesanBB);
        }

        // GET: PesanBBs/Create
        public IActionResult Create()
        {
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "NamaSupplier");
            ViewData["BahanBakuId"] = new SelectList(_context.BahanBaku, "Id", "NamaBahan");
            return View();
        }

        // POST: PesanBBs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: PesanBBs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BahanBakuId,StokBB,SatuanBB,SupplierId")] PesanBB pesanBB)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pesanBB);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Pesanan berhasil dikirim.";
                return RedirectToAction("Index", "BahanBakus"); // Specify the controller and action to redirect to
            }
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "NamaSupplier", pesanBB.SupplierId);
            ViewData["BahanBakuId"] = new SelectList(_context.BahanBaku, "Id", "NamaBahan", pesanBB.SupplierId);
            return View(pesanBB);
        }


        // GET: PesanBBs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PesanBB == null)
            {
                return NotFound();
            }

            var pesanBB = await _context.PesanBB.FindAsync(id);
            if (pesanBB == null)
            {
                return NotFound();
            }
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "NamaSupplier", pesanBB.SupplierId);
            ViewData["BahanBakuId"] = new SelectList(_context.BahanBaku, "Id", "NamaBahan", pesanBB.SupplierId);
            return View(pesanBB);
        }

        // POST: PesanBBs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BahanBakuId,StokBB,SatuanBB,SupplierId")] PesanBB pesanBB)
        {
            if (id != pesanBB.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pesanBB);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PesanBBExists(pesanBB.Id))
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
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "NamaSupplier", pesanBB.SupplierId);
            ViewData["BahanBakuId"] = new SelectList(_context.BahanBaku, "Id", "NamaBahan", pesanBB.SupplierId);
            return View(pesanBB);
        }

        // GET: PesanBBs/Delete/5
        public async Task<IActionResult> Delete(int? id, string dataAction)
        {

            if (id == null || _context.PesanBB == null)
            {
                return NotFound();
            }

            var pesanBB = await _context.PesanBB
                .Include(p => p.Supplier)
                .Include(p => p.BahanBaku)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pesanBB == null)
            {
                return NotFound();
            }
           
            if (dataAction == "accept")
            {
                // Pesanan diterima
                TempData["SuccessMessage"] = "Pesanan diterima.";
            }
            else if (dataAction == "reject")
            {
                // Pesanan ditolak
                TempData["SuccessMessage"] = "Pesanan ditolak.";
            }

            return View(pesanBB);
        }

        // POST: PesanBBs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PesanBB == null)
            {
                return Problem("Entity set 'KelCVBajaDivaManufakturContext.PesanBB'  is null.");
            }
            var pesanBB = await _context.PesanBB.FindAsync(id);
            if (pesanBB != null)
            {
                _context.PesanBB.Remove(pesanBB);
            }

           

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PesanBBExists(int id)
        {
          return (_context.PesanBB?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
