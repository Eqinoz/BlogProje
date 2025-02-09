using Microsoft.AspNetCore.Mvc;
using ProjectGSO.DbContext;

namespace ProjectGSO.Controllers
{
    public class BlogController : Controller
    {
        private readonly EfBlog _efBlog;

        public BlogController(BlogDbContext context)
        {
            _efBlog = new EfBlog();
        }

        public IActionResult Index()
        {
            var blogs= _efBlog.GetDetailBlog().Where(x=>x.Status==true).OrderByDescending(y=>y.Id).ToList();
            return View(blogs);
        }

        public IActionResult Details(int id)
        {
            var blogs = _efBlog.GetDetailBlog().Where(x => x.Id == id).FirstOrDefault();
            return View(blogs);
        }
    }
}
