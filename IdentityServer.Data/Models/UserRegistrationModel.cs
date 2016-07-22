using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Data.Models
{
    public class UserRegistrationModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string RepeatPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
