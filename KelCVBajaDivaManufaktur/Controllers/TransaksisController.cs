﻿using System;
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
    public class TransaksisController : Controller
    {
        private readonly KelCVBajaDivaManufakturContext _context;

        public TransaksisController(KelCVBajaDivaManufakturContext context)
        {
            _context = context;
        }

        // GET: Transaksis
        public async Task<IActionResult> Index(string searchString)
        {
            if (_context.Transaksi == null)
            {
                return Problem("Entity set 'KelCVBajaDivaManufakturContext.Transaksi'  is null.");
            }

            var transaksis = from m in _context.Transaksi.Include(d => d.Produk)
                             select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                transaksis = transaksis.Where(s => s.NamaPelanggan!.Contains(searchString));
            }

            return View(await transaksis.ToListAsync());
        }

        // GET: Transaksis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Transaksi == null)
            {
                return NotFound();
            }

            var transaksi = await _context.Transaksi.Include(d => d.Produk)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaksi == null)
            {
                return NotFound();
            }

            return View(transaksi);
        }

        // GET: Transaksis/Create
        public IActionResult Create()
        {
            ViewBag.HargaData = _context.Produk.ToDictionary(p => p.Id.ToString(), p => p.Harga);
            ViewData["ProdukId"] = new SelectList(_context.Produk, "Id", "NamaProduk");
            return View();
        }

        // POST: Transaksis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NamaPelanggan,ProdukId,TanggalBeli,Jumlah,TotalBayar,CaraBayar")] Transaksi transaksi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaksi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProdukId"] = new SelectList(_context.Produk, "Id", "NamaProduk", transaksi.ProdukId);
            return View(transaksi);
        }

        // GET: Transaksis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Transaksi == null)
            {
                return NotFound();
            }

            var transaksi = await _context.Transaksi.FindAsync(id);
            if (transaksi == null)
            {
                return NotFound();
            }
            ViewBag.HargaData = _context.Produk.ToDictionary(p => p.Id.ToString(), p => p.Harga);
            ViewData["ProdukId"] = new SelectList(_context.Produk, "Id", "NamaProduk");
            return View(transaksi);
        }

        // POST: Transaksis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NamaPelanggan,ProdukId,TanggalBeli,Jumlah,TotalBayar,CaraBayar")] Transaksi transaksi)
        {
            ViewBag.HargaData = _context.Produk.ToDictionary(p => p.Id.ToString(), p => p.Harga);

            if (id != transaksi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaksi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransaksiExists(transaksi.Id))
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
            ViewData["ProdukId"] = new SelectList(_context.Produk, "Id", "NamaProduk", transaksi.ProdukId);
            return View(transaksi);
        }

        // GET: Transaksis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Transaksi == null)
            {
                return NotFound();
            }

            var transaksi = await _context.Transaksi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaksi == null)
            {
                return NotFound();
            }

            return View(transaksi);
        }

        // POST: Transaksis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transaksi == null)
            {
                return Problem("Entity set 'KelCVBajaDivaManufakturContext.Transaksi'  is null.");
            }
            var transaksi = await _context.Transaksi.FindAsync(id);
            if (transaksi != null)
            {
                _context.Transaksi.Remove(transaksi);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransaksiExists(int id)
        {
          return (_context.Transaksi?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
