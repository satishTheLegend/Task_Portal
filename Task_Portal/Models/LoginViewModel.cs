using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Task_Portal.Models
{
    public class LoginViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required] [PasswordPropertyText]
        [Display(Name ="Password")]
        public string LoginPassword { get; set; }
        public bool RememberMe { get; set; }
        public bool IsUserActive { get; set; }
        public DateTime LoginDate { get; set; } = DateTime.Now;
        public string UserToken { get; set; } = Guid.NewGuid().ToString();
    }
}