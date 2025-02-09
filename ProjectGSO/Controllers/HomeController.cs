using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProjectGSO.DbContext;
using ProjectGSO.Models;

namespace ProjectGSO.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EfBlog _context;

        public HomeController(ILogger<HomeController> logger, BlogDbContext context)
        {
            _context = new EfBlog();
            _logger = logger;
        }

        public IActionResult Index()
        {
            var blogs = _context.GetDetailBlog().Where(x => x.Status == true).ToList();
            return View(blogs);
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
