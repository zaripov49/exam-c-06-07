using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.AccountDTO;

public class ResetPaswordDTO
{
    [Required]
    public string EmailOrUserName { get; set; } = null!;
    [Required]
    public string NewPassword { get; set; } = null!;
}
