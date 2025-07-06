using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.AccountDTO;

public class RegisterDTO
{
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string UserName { get; set; } = null!;
    [Required]
    [MinLength(4)]
    public string Password { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;
}
