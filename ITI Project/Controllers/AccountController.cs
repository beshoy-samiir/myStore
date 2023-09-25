using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ITI_Project.Data;
using ITI_Project.ViewModel;
using ITI_Project.Models;
using System.Runtime.ConstrainedExecution;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ITI_Project.Controllers
{
    public class AccountController : Controller
    {
        ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
        }
        public bool Find(string Email, string Password)
        {
            User user = _context.users.FirstOrDefault(u => u.Email == Email && u.Password == Password);
            if (user != null)
                return true;
            else
                return false;
        }
        public User GetUser(string Email)
        {
            User user =
               _context.users.FirstOrDefault(u => u.Email == Email);
            return user;
        }
        public List<string> GetRoles(int userId)
        {
            return _context.userRoles.Include(u => u.Role)
                .Where(u => u.UserID == userId)
                .Select(u => u.Role.Name).ToList();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel loginVM)
        {
            bool found = Find(loginVM.Email,loginVM.Password);
            if(found)
            {
                User user = GetUser(loginVM.Email);
                List<string> roles = GetRoles(user.Id);
                ClaimsIdentity Claims =
                    new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                Claims.AddClaim(new Claim(ClaimTypes.Email, loginVM.Email));
                Claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                Claims.AddClaim(new Claim("Email", user.Email));
                if (roles.Count > 0)
                {
                    Claims.AddClaim(new Claim(ClaimTypes.Role, roles[0]));
                }
                ClaimsPrincipal principal = new ClaimsPrincipal(Claims);

                HttpContext.SignInAsync
                    (CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("GetIndexView", "Product");
            }
            return View(loginVM);
        }
        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        public IActionResult Register()
        {
            return View("Register");
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            _context.users.Add(user);
            ClaimsIdentity Claims =
                    new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            Claims.AddClaim(new Claim(ClaimTypes.Name, user.Name));
            Claims.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            Claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            Claims.AddClaim(new Claim("Email", user.Email));
            ClaimsPrincipal principal = new ClaimsPrincipal(Claims);

            HttpContext.SignInAsync
                (CookieAuthenticationDefaults.AuthenticationScheme, principal);
            _context.SaveChanges();
            return RedirectToAction("GetIndexView", "Product");
        }


    }
}

