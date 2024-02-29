using Microsoft.AspNetCore.Mvc;
using s17_l3.Models;

namespace s17_l3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ScarpeDbContext _context;

        public HomeController(ScarpeDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var scarpe = _context.GetAll();
            return View(scarpe);
        }
    }
}