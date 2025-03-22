using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Progetto_S18_L5.Models;

namespace Progetto_S18_L5.ViewModels
{
    public class ReservationDeleteViewModel
    {
        public Guid ReservationId { get; set; }

        public string ClientId { get; set; }

        public Guid RoomId { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public bool State { get; set; }

        public string EmployeeId { get; set; }

        // navigazione
        public ApplicationUser? Client { get; set; }

        public Room? Room { get; set; }

        public ApplicationUser? Employee { get; set; }
    }
}
