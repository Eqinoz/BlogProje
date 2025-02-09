using Microsoft.AspNetCore.Mvc;
using ProjectGSO.DbContext;
using ProjectGSO.Models;

namespace ProjectGSO.Controllers
{
    public class ContentController : Controller
    {
        private readonly BlogDbContext _context;

        public ContentController(BlogDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage([Bind("Name,Phone,Email,Subject,Message")] Content content)
        {
            if (!ModelState.IsValid)
            {
                return View(content); // Hata varsa formu geri göster
            }

            content.PublishDateTime = DateTime.Now;
            _context.Content.Add(content);
            _context.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}
