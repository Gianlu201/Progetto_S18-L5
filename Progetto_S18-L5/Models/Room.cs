using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Progetto_S18_L5.Models
{
    public class Room
    {
        [Key]
        public Guid RoomId { get; set; }

        [Required]
        public required string RoomNumber { get; set; }

        [Required]
        public required Guid RoomTypeId { get; set; }

        [Required]
        public required decimal Price { get; set; }

        [Required]
        public required bool IsAvailable { get; set; }

        // navigazione
        [ForeignKey("RoomTypeId")]
        public RoomType RoomType { get; set; }

        public ICollection<Reservation>? Reservations { get; set; }
    }
}
