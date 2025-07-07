using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.AccountDTO;

public class ResetPaswordDTO
{
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string Token { get; set; } = null!;
    [Required]
    public string NewPassword { get; set; } = null!;
}
