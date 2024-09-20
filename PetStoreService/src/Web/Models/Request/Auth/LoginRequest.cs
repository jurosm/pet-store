using System.ComponentModel.DataAnnotations;

namespace PetStore.API.Models.Request.Auth
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}