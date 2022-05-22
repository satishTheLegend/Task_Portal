using System.ComponentModel.DataAnnotations;

namespace Task_Portal.Models
{
    public class UserRegistrationViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required] [Range(100000,999999,ErrorMessage ="Please Enter Valid Zipcode")]
        [Display(Name = "Zip Code")]
        public int ZipCode { get; set; }
        [Required][EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [RegularExpression(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$", ErrorMessage ="Please Enter Valid Phone")]
        public string Phone { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        public string Confirm_Password { get; set; }

    }
}
