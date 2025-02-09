using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ProjectGSO.DbContext;
using ProjectGSO.Models;
using ProjectGSO.Models.DTO;

namespace ProjectGSO.Controllers
{
    public class AdminController : Controller
    {
        private readonly EfBlog _blog;
        private readonly BlogDbContext _blogDbContext;

        public AdminController(BlogDbContext context)
        {
            _blog = new EfBlog();
            _blogDbContext = context;
        }
        
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Role")!= "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Table()
        {
            return View();
        }

        public IActionResult BlogAdd()
        {
            return View();
        }

        public IActionResult DeleteBlog(int id)
        {
            var result = _blogDbContext.Blogs.Where(x => x.Id == id).FirstOrDefault();
            _blogDbContext.Blogs.Remove(result);
            _blogDbContext.SaveChanges();
            return RedirectToAction("Blogs");
        }

        public IActionResult BlogEdit(int id)
        {
            var blog = _blog.GetDetailBlog().Where(x => x.Id == id).FirstOrDefault();
            return View(blog);
        }

        [HttpPost]
        public IActionResult BlogUpdate(BlogDTO blogDto)
        {
            var result = _blogDbContext.Blogs.Where(x => x.Id == blogDto.Id).FirstOrDefault();
            result.Title=blogDto.Title;
            result.ImageUrl=blogDto.ImageUrl;
            result.Description=blogDto.Description;
            result.Text=blogDto.Text;
            result.Tags=blogDto.Tags;
            _blogDbContext.SaveChanges();

            return RedirectToAction("Blogs");

        }

        public IActionResult Login()
        {
            return View();
        }
        //Tüm Blogları Blogs Sayfasına Getiriyor.
        public IActionResult Blogs()
        {
            var result = _blog.GetDetailBlog().OrderByDescending(x=>x.Id).ToList();
            return View(result);
        }
        //BlogAdd Buton İle Blog Ekleme
        [HttpPost]
        public IActionResult AddBlog(BlogDTO blog)
        {
            Blog result = new Blog();
            result.AuthorId = 2;
            result.Title = blog.Title;
            result.Description = blog.Description;
            result.Text = blog.Text;
            result.ImageUrl = blog.ImageUrl;
            result.AddeDateTime = DateTime.Now;
            result.Tags = blog.Tags;
            result.ViewCount = 0;
            result.CommentCount = 0;
            result.LikeCount = 0;


            _blogDbContext.Blogs.Add(result);
            _blogDbContext.SaveChanges();
            return RedirectToAction("/Admin/Blogs");
        }
        //Statü Değiştirme
        public IActionResult ChangeStatus(int id)
        {
            var result = _blogDbContext.Blogs.Where(x => x.Id == id).FirstOrDefault();
            if (result !=null)
            {
                result.Status = !result.Status;
                _blogDbContext.SaveChanges(result.Status);

            }


            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
