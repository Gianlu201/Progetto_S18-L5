using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Progetto_S18_L5.Models
{
    public class Reservation
    {
        [Key]
        public Guid ReservationId { get; set; }

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

        // navigazione
        [ForeignKey("ClientId")]
        public ApplicationUser Client { get; set; }

        [ForeignKey("RoomId")]
        public Room Room { get; set; }
    }
}
