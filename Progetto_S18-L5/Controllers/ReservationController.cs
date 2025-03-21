using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Progetto_S18_L5.Models;

namespace Progetto_S18_L5.Controllers
{
    public class ReservationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ReservationController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Add()
        {
            var clientsList = await _userManager
                .Users.Include(u => u.ApplicationUserRole)
                .Where(u => u.ApplicationUserRole == null || u.ApplicationUserRole.Count == 0)
                .ToListAsync();

            ViewBag.Clients = clientsList;

            return View();
        }
    }
}
