using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KelCVBajaDivaManufaktur.Data;
using KelCVBajaDivaManufaktur.Models;
using Microsoft.Extensions.Hosting;

namespace KelCVBajaDivaManufaktur.Controllers
{
    public class ProduksController : Controller
    {
        private readonly KelCVBajaDivaManufakturContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProduksController(KelCVBajaDivaManufakturContext context)
        {
            _context = context;
        }

        // GET: Produks
        public async Task<IActionResult> Index(string searchString)
        {
            if (_context.Produk == null)
            {
                return Problem("Entity set 'KelCVBajaDivaManufakturContext.Produk'  is null.");
            }

            var produks = from m in _context.Produk
                             select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                produks = produks.Where(s => s.NamaProduk!.Contains(searchString));
            }

            return View(await produks.ToListAsync());
        }

        // GET: Produks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Produk == null)
            {
                return NotFound();
            }

            var produk = await _context.Produk
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produk == null)
            {
                return NotFound();
            }

            return View(produk);
        }

        // GET: Produks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NamaProduk,GambarProduk,Stok,Harga,Kategori,Deskripsi")] Produk produk, IFormFile GambarProduk)
        {
            if (ModelState.IsValid)
            {
                if (GambarProduk != null && GambarProduk.Length > 0)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(GambarProduk.FileName);
                    string extension = Path.GetExtension(GambarProduk.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await GambarProduk.CopyToAsync(fileStream);
                    }

                    // Set nama file di model Produk
                    produk.GambarProduk = fileName;
                }

                _context.Add(produk);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produk);
        }

        // GET: Produks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Produk == null)
            {
                return NotFound();
            }

            var produk = await _context.Produk.FindAsync(id);
            if (produk == null)
            {
                return NotFound();
            }
            return View(produk);
        }

        // POST: Produks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NamaProduk,GambarProduk,Stok,Harga,Kategori,Deskripsi")] Produk produk)
        {
            if (id != produk.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produk);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdukExists(produk.Id))
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
            return View(produk);
        }

        // GET: Produks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Produk == null)
            {
                return NotFound();
            }

            var produk = await _context.Produk
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produk == null)
            {
                return NotFound();
            }

            return View(produk);
        }

        // POST: Produks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Produk == null)
            {
                return Problem("Entity set 'KelCVBajaDivaManufakturContext.Produk'  is null.");
            }
            var produk = await _context.Produk.FindAsync(id);
            if (produk != null)
            {
                _context.Produk.Remove(produk);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdukExists(int id)
        {
          return (_context.Produk?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
