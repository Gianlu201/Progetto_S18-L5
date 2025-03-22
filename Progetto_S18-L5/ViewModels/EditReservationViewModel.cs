using System.ComponentModel.DataAnnotations;
using Progetto_S18_L5.Models;

namespace Progetto_S18_L5.ViewModels
{
    public class EditReservationViewModel
    {
        public string ReservationId { get; set; }

        [Required]
        [Display(Name = "Client")]
        public required string ClientId { get; set; }

        [Display(Name = "Room")]
        public string? RoomId { get; set; }

        [Required]
        [Display(Name = "Check-in")]
        public required DateTime CheckIn { get; set; }

        [Required]
        [Display(Name = "Check-out")]
        public required DateTime CheckOut { get; set; }

        [Required]
        [Display(Name = "State")]
        public required bool State { get; set; }

        // navigazione
        public Room? Room { get; set; }
    }
}
