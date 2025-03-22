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
                    .OrderByDescending(r => r.State)
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

                var room = _context.Rooms.FirstOrDefault(r => r.RoomId == addReservation.RoomId);

                if (room == null)
                {
                    return false;
                }

                _context.Reservations.Add(reservation);

                room.IsAvailable = false;

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
                    .Include(r => r.Room)
                    .ThenInclude(room => room.RoomType)
                    .FirstOrDefaultAsync(r => r.ReservationId == id);

                if (reservation == null)
                {
                    return new EditReservationViewModel()
                    {
                        CheckIn = DateTime.Now,
                        CheckOut = DateTime.Now,
                        ClientId = "",
                        RoomId = "",
                        State = false,
                        ReservationId = "",
                    };
                }

                return new EditReservationViewModel()
                {
                    CheckIn = reservation.CheckIn,
                    CheckOut = reservation.CheckOut,
                    ClientId = reservation.ClientId,
                    RoomId = reservation.RoomId.ToString(),
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
                    RoomId = "",
                    State = false,
                    ReservationId = "",
                };
            }
        }

        public async Task<bool> EditReservation(
            EditReservationViewModel editReservation,
            string userId
        )
        {
            try
            {
                var reservation = await _context.Reservations.FirstOrDefaultAsync(r =>
                    r.ReservationId == Guid.Parse(editReservation.ReservationId)
                );

                if (reservation == null)
                {
                    return false;
                }

                reservation.CheckIn = editReservation.CheckIn;
                reservation.CheckOut = editReservation.CheckOut;
                reservation.ClientId = editReservation.ClientId;
                reservation.State = editReservation.State;
                reservation.EmployeeId = userId;

                // il controllo di modifica è andato a diventare sempre più contorto, ma è necessario per verificare che venga
                // effettivamente inserita una nuova stanza e quindi va a modificare la stanza associata alla prenotazione
                // se e solo se gliene viene assegnata una nuova, altrimenti il controllo passa oltre
                if (
                    editReservation.RoomId != null
                    && Guid.TryParse(editReservation.RoomId, out Guid newRoomId)
                    && reservation.RoomId.ToString() != editReservation.RoomId
                )
                {
                    var oldRoom = await _context.Rooms.FirstOrDefaultAsync(r =>
                        r.RoomId == reservation.RoomId
                    );
                    var newRoom = await _context.Rooms.FirstOrDefaultAsync(r =>
                        r.RoomId.ToString() == editReservation.RoomId
                    );
                    if (oldRoom != null && newRoom != null)
                    {
                        oldRoom.IsAvailable = true;
                        newRoom.IsAvailable = false;

                        reservation.RoomId = Guid.Parse(editReservation.RoomId);
                    }
                    else
                    {
                        return false;
                    }
                }

                if (reservation.State == false)
                {
                    var room = await _context.Rooms.FirstOrDefaultAsync(r =>
                        r.RoomId == reservation.RoomId
                    );

                    if (room != null)
                    {
                        room.IsAvailable = true;
                    }
                }

                return await TrySaveAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<ReservationDeleteViewModel> GetReservationForDeleteByIdAsync(Guid id)
        {
            try
            {
                var reservation = await _context
                    .Reservations.Include(r => r.Client)
                    .Include(r => r.Employee)
                    .Include(r => r.Room)
                    .FirstOrDefaultAsync(r => r.ReservationId == id);

                if (reservation == null)
                {
                    return new ReservationDeleteViewModel();
                }

                return new ReservationDeleteViewModel()
                {
                    CheckIn = reservation.CheckIn,
                    CheckOut = reservation.CheckOut,
                    ClientId = reservation.ClientId,
                    RoomId = reservation.RoomId,
                    State = reservation.State,
                    EmployeeId = reservation.EmployeeId,
                    ReservationId = reservation.ReservationId,
                    Client = reservation.Client,
                    Room = reservation.Room,
                    Employee = reservation.Employee,
                };
            }
            catch
            {
                return new ReservationDeleteViewModel();
            }
        }

        public async Task<bool> DeleteReservation(Guid id)
        {
            try
            {
                var reservation = await _context.Reservations.FirstOrDefaultAsync(r =>
                    r.ReservationId == id
                );

                if (reservation == null)
                {
                    return false;
                }

                _context.Reservations.Remove(reservation);

                return await TrySaveAsync();
            }
            catch
            {
                return false;
            }
        }
    }
}
