using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Progetto_S18_L5.Models;
using Progetto_S18_L5.Services;
using Progetto_S18_L5.ViewModels;

namespace Progetto_S18_L5.Controllers
{
    public class ReservationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ReservationService _reservationService;
        private readonly RoomService _roomService;

        public ReservationController(
            UserManager<ApplicationUser> userManager,
            ReservationService reservationService,
            RoomService roomService
        )
        {
            _userManager = userManager;
            _reservationService = reservationService;
            _roomService = roomService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservationsList = await _reservationService.GetAllReservationAsync();

            return PartialView("_ReservationsList", reservationsList);
        }

        public async Task<IActionResult> Add()
        {
            var clientsList = await _userManager
                .Users.Include(u => u.ApplicationUserRole)
                .Where(u => u.ApplicationUserRole == null || u.ApplicationUserRole.Count == 0)
                .ToListAsync();

            ViewBag.Clients = clientsList;

            var roomsList = await _roomService.GetAvailableRoomsAsync();

            ViewBag.RoomsAvailable = roomsList;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddReservationViewModel addReservation)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid data!";
                return View(addReservation);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _reservationService.AddReservation(addReservation, userId);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Reservation/Edit/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var response = await _reservationService.GetReservationByIdAsync(id);

            if (response.ReservationId == "")
            {
                TempData["Error"] = "Reservation not found!";
                return RedirectToAction("Index");
            }

            var clientsList = await _userManager
                .Users.Include(u => u.ApplicationUserRole)
                .Where(u => u.ApplicationUserRole == null || u.ApplicationUserRole.Count == 0)
                .ToListAsync();

            ViewBag.Clients = clientsList;

            var roomsList = await _roomService.GetAvailableRoomsAsync();

            ViewBag.RoomsAvailable = roomsList;

            return PartialView("_ReservationEditModal", response);
        }

        [HttpPost("Reservation/EditSave")]
        public async Task<IActionResult> EditSave(EditReservationViewModel editReservation)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid data!";
                return RedirectToAction("Index");
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _reservationService.EditReservation(editReservation, userId);

            if (!result)
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }

        [HttpGet("Reservation/Delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var reservation = await _reservationService.GetReservationForDeleteByIdAsync(id);

            if (reservation.State == null)
            {
                return RedirectToAction("Index");
            }

            return PartialView("_ReservationDeleteModal", reservation);
        }

        [HttpPost("Reservation/DeleteConfirm/{id:guid}")]
        public async Task<IActionResult> DeleteConfirm(Guid id)
        {
            var result = await _reservationService.DeleteReservation(id);

            if (!result)
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }
    }
}
