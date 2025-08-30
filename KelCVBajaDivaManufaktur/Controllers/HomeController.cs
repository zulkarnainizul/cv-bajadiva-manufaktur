using KelCVBajaDivaManufaktur.Data;
using KelCVBajaDivaManufaktur.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace KelCVBajaDivaManufaktur.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly KelCVBajaDivaManufakturContext _context;

        public HomeController(ILogger<HomeController> logger, KelCVBajaDivaManufakturContext context)
        {
            _logger = logger;
            _context = context;
        }

        private ProductSalesViewModel GetProductSales()
        {
            var result = _context.Transaksi
                .GroupBy(t => t.Produk.NamaProduk)
                .Select(g => new
                {
                    ProductName = g.Key,
                    TotalSales = g.Sum(t => t.Jumlah * t.Produk.Harga)
                })
                .ToList();

            var productSales = new ProductSalesViewModel
            {
                ProductNames = result.Select(r => r.ProductName).ToList(),
                TotalSales = result.Select(r => r.TotalSales).ToList()
            };

            return productSales;
        }

        public IActionResult Index()
        {
            var model = new DashboardViewModel
            {
                JumlahTransaksi = _context.Transaksi.Count(),
                JumlahProduk = _context.Produk.Count(),
                JumlahBahanBaku = _context.BahanBaku.Count(),
                JumlahSupplier = _context.Supplier.Count(),
                TransaksiList = _context.Transaksi
                    .Include(t => t.Produk)
                    .ToList(),
                PesanBBList = _context.PesanBB
                    .Include(t => t.Supplier)
                    .Include(t => t.BahanBaku)
                    .ToList(),
                TotalBayarSumByMonth = GetTotalBayarSumByMonth(),
                ProductSales = GetProductSales(),
            };

            return View(model);

        }
        private List<decimal> GetTotalBayarSumByMonth()
        {
            var result = _context.Transaksi
                .GroupBy(t => t.TanggalBeli.Month)
                .OrderBy(g => g.Key)
                .Select(g => g.Sum(t => t.TotalBayar))
                .ToList();

            return result;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}