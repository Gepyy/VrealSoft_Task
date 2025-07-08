using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VrealSoft_Task.Models;

[Table("User")]
public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public string Login { get; set; }
    [Required]
    [StringLength(100)]
    public string Password { get; set; }
}