using System.ComponentModel.DataAnnotations;

namespace Progetto_S18_L5.ViewModels
{
    public class AddReservationViewModel
    {
        [Required]
        public required string ClientId { get; set; }

        [Required]
        public required Guid RoomId { get; set; }

        [Required]
        public required DateTime CheckIn { get; set; }

        [Required]
        public required DateTime CheckOut { get; set; }

        [Required]
        public required bool State { get; set; }
    }
}
