using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Progetto_S18_L5.Models;
using Progetto_S18_L5.Services;
using Progetto_S18_L5.ViewModels;

namespace Progetto_S18_L5.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly AccountService _accountService;

        public static string UserId = "";

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            AccountService accountService
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _accountService = accountService;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Manage()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

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
                new Claim(ClaimTypes.NameIdentifier, user.Id),
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

        [Authorize]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> RegisterEmployee()
        {
            var rolesList = new RolesListViewModel();

            rolesList.Roles = await _roleManager.Roles.ToListAsync();

            ViewBag.Roles = rolesList;

            return View();
        }

        [Authorize]
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> RegisterEmployee(
            RegisterEmployeeViewModel registerEmployeeViewModel
        )
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid data";
                return View(registerEmployeeViewModel);
            }

            var employeeId = Guid.NewGuid().ToString();

            var newUser = new ApplicationUser()
            {
                Id = employeeId,
                FirstName = registerEmployeeViewModel.FirstName,
                LastName = registerEmployeeViewModel.LastName,
                UserName = registerEmployeeViewModel.Email,
                Email = registerEmployeeViewModel.Email,
                PhoneNumber = registerEmployeeViewModel.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(
                newUser,
                registerEmployeeViewModel.Password
            );

            if (!result.Succeeded)
            {
                TempData["Error"] = "Error creating user";
                return View(registerEmployeeViewModel);
            }

            //await _userManager.AddToRoleAsync(user, "Employee");
            //await _userManager.AddToRoleAsync(user, "Manager");

            var user = await _userManager.FindByEmailAsync(registerEmployeeViewModel.Email);

            if (user == null)
            {
                TempData["Error"] = "Error creating user";
                return View(registerEmployeeViewModel);
            }

            var role = await _roleManager.FindByIdAsync(registerEmployeeViewModel.RoleId);

            if (role == null || role.Name == null)
            {
                TempData["Error"] = "Error while inserting the role!";
                return View(registerEmployeeViewModel);
            }

            var addingResult = await _accountService.AddUserRole(user, role);

            if (!addingResult)
            {
                TempData["Error"] = "Error while inserting the role!";
                return View(registerEmployeeViewModel);
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
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

        [Authorize(Roles = "Admin")]
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

        [Authorize]
        [HttpGet("Account/EditClient/{id:guid}")]
        public async Task<IActionResult> EditClient(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                TempData["Error"] = "User not found";
                return RedirectToAction("Index", "Home");
            }

            var editClientViewModel = new EditClientViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
            };

            return PartialView("_ClientEditModal", editClientViewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditClient(EditClientViewModel editClient)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Error while updating client!";
                return Json(new { success = false });
            }

            var result = await _accountService.EditClientAsync(editClient);

            if (!result)
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }

        [Authorize]
        [HttpGet("Account/DeleteClient/{id:guid}")]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            var client = await _userManager.FindByIdAsync(id.ToString());

            if (client == null)
            {
                TempData["Error"] = "User not found";
                return RedirectToAction("Index", "Home");
            }

            var resultUser = new UserDeleteViewModel()
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
            };

            return PartialView("_UserDeleteModal", resultUser);
        }

        [Authorize]
        [HttpPost("Account/ConfirmDeleteClient/{id:guid}")]
        public async Task<IActionResult> ConfirmDeleteClient(Guid id)
        {
            var client = await _userManager.FindByIdAsync(id.ToString());

            if (client == null)
            {
                TempData["Error"] = "User not found";
                return RedirectToAction("Index", "Home");
            }

            var result = await _userManager.DeleteAsync(client);

            if (!result.Succeeded)
            {
                TempData["Error"] = "Error while deleting client!";
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Account/EditEmployee/{id:guid}")]
        public async Task<IActionResult> EditEmployee(Guid id)
        {
            var user = await _userManager
                .Users.Include(u => u.ApplicationUserRole)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id.ToString());

            if (user == null)
            {
                TempData["Error"] = "User not found";
                return RedirectToAction("Index", "Home");
            }

            var editEmployee = new EditEmployeeViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                RoleId = user.ApplicationUserRole.FirstOrDefault().RoleId,
            };

            var rolesList = new RolesListViewModel();

            rolesList.Roles = await _roleManager.Roles.ToListAsync();

            ViewBag.Roles = rolesList;

            return PartialView("_EmployeeEditModal", editEmployee);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditEmployee(EditEmployeeViewModel editEmployee)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Error while updating employee!";
                return Json(new { success = false });
            }

            var oldEmp = _userManager
                .Users.Include(u => u.ApplicationUserRole)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefault(u => u.Id == editEmployee.Id);

            var result = await _accountService.EditEmployeeAsync(editEmployee, oldEmp);

            if (!result)
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Account/DeleteEmployee/{id:guid}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var employee = await _userManager.FindByIdAsync(id.ToString());
            if (employee == null)
            {
                return RedirectToAction("Index");
            }

            var resultUser = new UserDeleteViewModel()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
            };
            return PartialView("_UserDeleteModal", resultUser);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Account/DeleteEmployeeConfirm/{id:guid}")]
        public async Task<IActionResult> DeleteEmployeeConfirm(Guid id)
        {
            var employee = await _userManager.FindByIdAsync(id.ToString());
            if (employee == null)
            {
                return RedirectToAction("Index");
            }
            var result = await _userManager.DeleteAsync(employee);
            if (!result.Succeeded)
            {
                TempData["Error"] = "Error while deleting employee!";
                return Json(new { success = false });
            }
            return Json(new { success = true });
        }
    }
}
