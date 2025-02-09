using Microsoft.AspNetCore.Mvc;
using ProjectGSO.Services.Abstrack;
using ProjectGSO.Services.Concrete;

namespace ProjectGSO.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
         public IActionResult Register() => View();
         public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Register(string firstName, string lastName, string email, string password)
        {
            if (_authService.Register(firstName, lastName, email, password))
            {
                return RedirectToAction("Login");
            }
            ViewBag.Error = "Bu Mail Adresi Zaten Kullanımda";
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _authService.Login(email, password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("UserName", user.FirstName + " " + user.LastName);
                HttpContext.Session.SetString("Role", user.Role);
                return RedirectToAction("Index","Home");
            }
            ViewBag.Error = "Mail Adresi veya Şifre Hatalı";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
