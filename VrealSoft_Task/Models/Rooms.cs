namespace VrealSoft_Task.Models;

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Rooms")]
public class Rooms
{
    [Key]
    public int RoomID { get; set; }
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    [Range(1, int.MaxValue)]
    public int Capacity { get; set; }
}