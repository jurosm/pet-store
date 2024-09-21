using System.ComponentModel.DataAnnotations;

namespace PetStoreService.Application.Models.Request.Auth;

public class LoginRequest
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
