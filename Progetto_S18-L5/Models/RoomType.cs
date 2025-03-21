using System.ComponentModel.DataAnnotations;

namespace Progetto_S18_L5.Models
{
    public class RoomType
    {
        [Key]
        public Guid RoomTypeId { get; set; }

        [Required]
        public required string RoomTypeName { get; set; }

        [Required]
        public required int MaxOccupancy { get; set; }

        // navigazione
        public ICollection<Room>? Rooms { get; set; }
    }
}
