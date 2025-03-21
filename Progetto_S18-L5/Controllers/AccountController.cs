using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Progetto_S18_L5.Models;
using Progetto_S18_L5.ViewModels;

namespace Progetto_S18_L5.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public static string UserId = "";

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Manage()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid datas";
                return View(loginViewModel);
            }

            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

            if (user == null)
            {
                TempData["Error"] = "Invalid email or password";
                return View(loginViewModel);
            }

            var signInResult = await _signInManager.PasswordSignInAsync(
                user,
                loginViewModel.Password,
                false,
                false
            );

            if (!signInResult.Succeeded)
            {
                TempData["Error"] = "Invalid email or password";
                return View(loginViewModel);
            }

            var roles = await _signInManager.UserManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FirstName ?? string.Empty),
                new Claim(ClaimTypes.Surname, user.LastName ?? string.Empty),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                // salvo l'id dell'utente loggato in un claim
                new Claim(UserId, user.Id),
                new Claim(ClaimTypes.SerialNumber, user.Id),
                new Claim(ClaimTypes.DateOfBirth, user.Id),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity)
            );

            UserId = user.Id;

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult RegisterEmployee()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid data";
                return View(registerViewModel);
            }

            var newUser = new ApplicationUser()
            {
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                UserName = registerViewModel.Email,
                Email = registerViewModel.Email,
                PhoneNumber = registerViewModel.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (!result.Succeeded)
            {
                TempData["Error"] = "Error creating user";
                return View(registerViewModel);
            }

            var user = await _userManager.FindByEmailAsync(registerViewModel.Email);

            if (user == null)
            {
                TempData["Error"] = "Error creating user";
                return View(registerViewModel);
            }

            //await _userManager.AddToRoleAsync(user, "Employee");
            //await _userManager.AddToRoleAsync(user, "Manager");

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            var usersList = new UsersListViewModel();
            usersList.Users = await _userManager
                .Users.Include(u => u.ApplicationUserRole)
                .Where(u => u.ApplicationUserRole == null || u.ApplicationUserRole.Count == 0)
                .ToListAsync();

            return PartialView("_ClientsList", usersList);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            var usersList = new UsersListViewModel();

            usersList.Users = await _userManager
                .Users.Include(u => u.ApplicationUserRole)
                .ThenInclude(ur => ur.Role)
                .Where(u => u.ApplicationUserRole != null && u.ApplicationUserRole.Count > 0)
                .ToListAsync();

            return PartialView("_EmployeesList", usersList);
        }
    }
}
