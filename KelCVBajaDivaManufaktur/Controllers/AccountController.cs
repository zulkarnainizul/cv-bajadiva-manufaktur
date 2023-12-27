using KelCVBajaDivaManufaktur.Data;
using KelCVBajaDivaManufaktur.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KelCVBajaDivaManufaktur.Controllers
{
    // AccountController.cs
    public class AccountController : Controller
    {
        private readonly KelCVBajaDivaManufakturContext _context;
        private readonly IHttpContextAccessor _accessor;

        public IActionResult Login()
        {
            return View();
        }


    }

}
