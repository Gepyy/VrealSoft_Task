using System.ComponentModel.DataAnnotations;

namespace VrealSoft_Task.DTO;

public class UserDto
{
    [Required(ErrorMessage = "Login is required.")]
    [StringLength(100, ErrorMessage = "Login cannot be longer than 100 characters.")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, ErrorMessage = "Password cannot be longer than 100 characters.")]
    public string Password { get; set; }
} 