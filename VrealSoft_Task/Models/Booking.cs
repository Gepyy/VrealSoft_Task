using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VrealSoft_Task.Models;

[Table("Booking")]
public class Booking
{
    [Key]
    public int BookingId { get; set; }
    [Required]
    public DateTime StartTime { get; set; }
    [Required]
    public DateTime EndTime { get; set; }
    [Required]
    [StringLength(100)]
    public string BookedBy { get; set; }
    [ForeignKey("Room")]
    public int RoomId { get; set; }
    public Rooms Room { get; set; }
}