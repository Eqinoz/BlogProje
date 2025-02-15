using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ProjectGSO.DbContext;
using ProjectGSO.Models;
using ProjectGSO.Models.DTO;
using ProjectGSO.Models.ViewModel;

namespace ProjectGSO.Controllers
{
    public class AdminController : Controller
    {
        private readonly EfBlog _blog;
        private readonly EfUsers _users;
        private readonly BlogDbContext _blogDbContext;

        public AdminController(BlogDbContext context)
        {
            _blog = new EfBlog();
            _blogDbContext = context;
            _users = new EfUsers();
        }
        
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Role")!= "Admin" && HttpContext.Session.GetString("Role") != "Author")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Profile()
        {
            var result = _users.ByIdGetUsers(int.Parse(HttpContext.Session.GetString("UserId"))).FirstOrDefault();
            return View(result);
        }

        [HttpPost]
        public IActionResult ProfileUpdate(UsersDTO usersDto)
        {
            var role = _blogDbContext.Roles.Where(x => x.RoleName == usersDto.Role).FirstOrDefault();
            var result = _blogDbContext.Users.Where(x => x.Id == usersDto.Id).FirstOrDefault();
            result.FirstName = usersDto.FirstName;
            result.LastName = usersDto.LastName;
            result.Email = usersDto.Email;
            result.RoleId = role.Id;
            result.Username = usersDto.Username;
            if (usersDto.Password!=null)
            {
                result.Password = BCrypt.Net.BCrypt.HashPassword(usersDto.Password);
            }
            _blogDbContext.SaveChanges();
            return RedirectToAction("Table");
        }

        public IActionResult Table()
        {
           var result = _users.GetUsers();
           var role = _blogDbContext.Roles.ToList();
           var viewModel = new Role_User_ViewModel
           {
               Users = result,
               Roles = role
           };
            return View(viewModel);
        }
        public IActionResult Members()
        {
            var result = _blogDbContext.Members.ToList();
            return View(result);
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
            if (HttpContext.Session.GetString("Role")=="Admin")
            {
                var result = _blog.GetDetailBlog().ToList();
                return View(result);
            }
            else if (HttpContext.Session.GetString("Role") == "Author")
            {
                var result = _blog.ByUserIdGetBlog(int.Parse(HttpContext.Session.GetString("UserId"))).ToList();
                return View(result);
            }

            return null;

        }
        //BlogAdd Buton İle Blog Ekleme
        [HttpPost]
        public IActionResult AddBlog(BlogDTO blog)
        {
            var userId= HttpContext.Session.GetString("UserId");
            Blog result = new Blog();
            result.AuthorId = int.Parse(userId);
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
            return RedirectToAction("Blogs");
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

        

        public IActionResult AddMember()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MemberAdd(Members members)
        {
            var member = new Members();
            member.CompanyName= members.CompanyName;
            member.Description = members.Description;
            member.ImageUrl = members.ImageUrl;
            _blogDbContext.Members.Add(member);
            _blogDbContext.SaveChanges();
            return RedirectToAction("Members");
        }
    }
}
