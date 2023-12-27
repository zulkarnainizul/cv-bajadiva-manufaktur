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

        public IActionResult Index()
        {
            var model = new DashboardViewModel
            {
                JumlahTransaksi = _context.Transaksi.Count(),
                JumlahProduk = _context.Produk.Count(),
                JumlahBahanBaku = _context.BahanBaku.Count(),
                JumlahSupplier = _context.Supplier.Count()
            };

            return View(model);

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