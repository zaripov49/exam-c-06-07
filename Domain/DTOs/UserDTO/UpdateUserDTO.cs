using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.UserDTO;

public class UpdateUserDTO
{
    public string Id { get; set; } = "";
    [Required]
    public string UserName { get; set; } = "";
    [Required]
    public string Email { get; set; } = "";
    [Required]
    [MinLength(4)]
    public string Password { get; set; } = "";
}
