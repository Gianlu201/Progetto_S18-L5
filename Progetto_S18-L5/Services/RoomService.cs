using Microsoft.EntityFrameworkCore;
using Progetto_S18_L5.Data;
using Progetto_S18_L5.Models;
using Progetto_S18_L5.ViewModels;

namespace Progetto_S18_L5.Services
{
    public class RoomService
    {
        private readonly ApplicationDbContext _context;

        public RoomService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RoomsListViewModel> GetAvailableRoomsAsync()
        {
            try
            {
                var roomsList = new RoomsListViewModel();

                roomsList.Rooms = await _context
                    .Rooms.Include(r => r.RoomType)
                    .Where(r => r.IsAvailable)
                    .ToListAsync();

                return roomsList;
            }
            catch
            {
                return new RoomsListViewModel() { Rooms = new List<Room>() };
            }
        }
    }
}
