using Microsoft.AspNetCore.Mvc;
using ProjectGSO.DbContext;
using ProjectGSO.Models;

namespace ProjectGSO.Controllers
{
    public class MembersController : Controller
    {
        private readonly Members _members;
        private readonly BlogDbContext _context;

        public MembersController(BlogDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var result = _context.Members.ToList();
            return View(result);
        }
    }
}
