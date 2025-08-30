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
        public ProduksController(KelCVBajaDivaManufakturContext context, IWebHostEnvironment hc)
        {
            _context = context;
            _hostEnvironment = hc;
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

            // Menambahkan informasi gambar ke ViewData
            ViewData["ImagePath"] = Path.Combine("/images", produk.GambarProduk);

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
        public async Task<IActionResult> Create(ProductViewModel product1)
        {
            if (ModelState.IsValid) // Validasi model
            {
                String filename = "";
                if (product1.photo != null)
                {
                    String uploadfolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                    filename = Guid.NewGuid().ToString() + "_" + product1.photo.FileName;
                    String filepath = Path.Combine(uploadfolder, filename);
                    product1.photo.CopyTo(new FileStream(filepath, FileMode.Create));
                }

                Produk p = new Produk
                {
                    NamaProduk = product1.NamaProduk,
                    GambarProduk = filename,
                    Stok = product1.Stok,
                    Harga = product1.Harga,
                    Kategori = product1.Kategori,
                    Deskripsi = product1.Deskripsi
                };

                _context.Produk.Add(p);
                await _context.SaveChangesAsync(); // Menggunakan SaveChangesAsync()

                ViewBag.Success = "Record added";

                // Alihkan ke action Index
                return RedirectToAction("Index");
            }

            // Jika model tidak valid, kembalikan ke view Create dengan model yang tidak valid
            return View(product1);
        }


        // GET: Produks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,NamaProduk,GambarProduk,Stok,Harga,Kategori,Deskripsi")] Produk produk, IFormFile photo)
        {
            if (id != produk.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    String filename = produk.GambarProduk; // Simpan nama gambar lama

                    if (photo != null)
                    {
                        String uploadfolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                        filename = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        String filepath = Path.Combine(uploadfolder, filename);
                        photo.CopyTo(new FileStream(filepath, FileMode.Create));

                        // Hapus gambar lama jika berhasil mengunggah yang baru
                        if (!string.IsNullOrEmpty(produk.GambarProduk))
                        {
                            String oldFilePath = Path.Combine(uploadfolder, produk.GambarProduk);
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }
                        }
                    }

                    produk.GambarProduk = filename;

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
