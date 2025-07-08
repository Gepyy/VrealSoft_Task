using System.ComponentModel.DataAnnotations;

namespace VrealSoft_Task.DTO;

public class RoomDto
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
    public string Name { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be a positive integer.")]
    public int Capacity { get; set; }
} 