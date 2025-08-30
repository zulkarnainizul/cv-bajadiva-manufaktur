using KelCVBajaDivaManufaktur.Data;
using KelCVBajaDivaManufaktur.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

namespace KelCVBajaDivaManufaktur.Controllers
{
    public class AuthController : Controller
    {
        private readonly KelCVBajaDivaManufakturContext _context;
        private readonly IHttpContextAccessor _accessor;

        public AuthController(KelCVBajaDivaManufakturContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login(string username, string password)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddMonths(1);

            if (_context.Pengguna == null)
            {
                return Problem("Entity set 'Pengguna' is null.");
            }

            var user = await _context.Pengguna
                .FirstOrDefaultAsync(m => m.Username == username);

            if (user!=null)
            {
                if (user.Password == password)
                {
                    if (user.HakAkses == "Admin")
                    {
                        Response.Cookies.Append("hak_akses", "Admin", options);
                        return RedirectToAction("Index", "Home");

                    }
                    else if (user.HakAkses == "Karyawan")
                    {
                        Response.Cookies.Append("hak_akses", "Karyawan", options);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        Response.Cookies.Append("hak_akses", "Supplier", options);
                        return RedirectToAction("Index", "PesanBBs");
                    }
                }
                else
                {
                    TempData["Message"] = "Password tidak sesuai!";
                }
            }
            else
            {
                TempData["Message"] = "Username tidak sesuai!";
            }

            return RedirectToAction("Index", "Auth");
        }

        public IActionResult Logout()
        {
            string user = _accessor.HttpContext.Request.Cookies["hak_akses"];


            if (user != null)
            {
                Response.Cookies.Delete("hak_akses");
                TempData["Message"] = "Anda Berhasil Keluar!";
                return RedirectToAction("Index", "Auth");
            }
            else
            {
                TempData["Message"] = "Gagal!";
                return RedirectToAction("Index", "Home");
            }
        }
    }

}
