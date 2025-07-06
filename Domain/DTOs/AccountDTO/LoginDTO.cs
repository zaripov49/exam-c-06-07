using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.AccountDTO;

public class LoginDTO
{
    [Required]
    public string UserName { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}
