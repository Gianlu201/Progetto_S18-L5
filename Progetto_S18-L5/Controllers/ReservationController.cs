﻿using System.Threading.Tasks;
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

            var result = await _reservationService.AddReservation(addReservation);

            return RedirectToAction("Index", "Home");
        }
    }
}
