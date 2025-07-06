using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.AccountDTO;

public class ChangePasswordDTO
{
    [Required]
    public string OldPassword { get; set; } = null!;
    [Required]
    public string NewPassword { get; set; } = null!;
    [Compare("NewPassword")]
    public string ConfirmPassword { get; set; } = null!;
}
