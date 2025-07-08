using System.ComponentModel.DataAnnotations;

namespace VrealSoft_Task.DTO;

public class BookingDto
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "RoomId must be a positive integer.")]
    public int RoomId { get; set; }

    [Required(ErrorMessage = "StartTime is required.")]
    [RegularExpression(@"^\d{2}:\d{2} \d{2}/\d{2}/\d{4}$", ErrorMessage = "StartTime must be in format 'HH:mm dd/MM/yyyy'.")]
    public string StartTime { get; set; }

    [Required(ErrorMessage = "EndTime is required.")]
    [RegularExpression(@"^\d{2}:\d{2} \d{2}/\d{2}/\d{4}$", ErrorMessage = "EndTime must be in format 'HH:mm dd/MM/yyyy'.")]
    public string EndTime { get; set; }
} 