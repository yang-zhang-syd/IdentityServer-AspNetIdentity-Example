using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Data.Models
{
    public class UpdateUserPasswordModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string RepeatPassword { get; set; }
    }
}
