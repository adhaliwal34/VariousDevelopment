#nullable disable

using System.ComponentModel.DataAnnotations;

namespace <ProjectName>.Models
{
    public class AccountLoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe  { get; set; }
    }
}
