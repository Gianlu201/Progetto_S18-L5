using Microsoft.EntityFrameworkCore;
using Progetto_S18_L5.Data;
using Progetto_S18_L5.Models;
using Progetto_S18_L5.ViewModels;

namespace Progetto_S18_L5.Services
{
    public class ReservationService
    {
        private readonly ApplicationDbContext _context;

        public ReservationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> TrySaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ReservationsListViewModel> GetAllReservationAsync()
        {
            try
            {
                var reservationsList = new ReservationsListViewModel();

                reservationsList.Reservations = await _context
                    .Reservations.Include(r => r.Client)
                    .Include(r => r.Room)
                    .Include(r => r.Employee)
                    .ToListAsync();

                return reservationsList;
            }
            catch
            {
                return new ReservationsListViewModel() { Reservations = new List<Reservation>() };
            }
        }

        public async Task<bool> AddReservation(
            AddReservationViewModel addReservation,
            string employeeId
        )
        {
            try
            {
                var reservation = new Reservation()
                {
                    ReservationId = Guid.NewGuid(),
                    ClientId = addReservation.ClientId,
                    RoomId = addReservation.RoomId,
                    CheckIn = addReservation.CheckIn,
                    CheckOut = addReservation.CheckOut,
                    State = addReservation.State,
                    EmployeeId = employeeId,
                };

                _context.Reservations.Add(reservation);

                return await TrySaveAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<EditReservationViewModel> GetReservationByIdAsync(Guid id)
        {
            try
            {
                var reservation = await _context
                    .Reservations.Include(r => r.Client)
                    .Include(r => r.Room)
                    .Include(r => r.Employee)
                    .FirstOrDefaultAsync(r => r.ReservationId == id);

                if (reservation == null)
                {
                    return new EditReservationViewModel()
                    {
                        CheckIn = DateTime.Now,
                        CheckOut = DateTime.Now,
                        ClientId = "",
                        RoomId = Guid.Empty,
                        State = false,
                        ReservationId = "",
                    };
                }

                return new EditReservationViewModel()
                {
                    CheckIn = reservation.CheckIn,
                    CheckOut = reservation.CheckOut,
                    ClientId = reservation.ClientId,
                    RoomId = reservation.RoomId,
                    State = reservation.State,
                    ReservationId = reservation.ReservationId.ToString(),
                };
            }
            catch
            {
                return new EditReservationViewModel()
                {
                    CheckIn = DateTime.Now,
                    CheckOut = DateTime.Now,
                    ClientId = "",
                    RoomId = Guid.Empty,
                    State = false,
                    ReservationId = "",
                };
            }
        }
    }
}
