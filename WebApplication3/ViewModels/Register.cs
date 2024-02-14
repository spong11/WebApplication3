using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;


namespace WebApplication3.ViewModels
{
    public class Register
    {

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Credit Card No")]
        [RegularExpression(@"^[0-9]{16}$", ErrorMessage = "Credit Card number must be 16 digits")]
        public string CreditCardNumber { get; set; }

        [Required]
        [Display(Name = "Mobile No")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be 10 digits")]
        public string MobileNumber { get; set; }

        [Required]
        [Display(Name = "Billing Address")]
        public string BillingAddress { get; set; }

        [Required]
        [Display(Name = "Shipping Address")]
        public string ShippingAddress { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required]
        [MinLength(12, ErrorMessage = "Enter at least 12 characters password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}$",
        ErrorMessage = "Passwords must be at least 12 characters long and contain at least an upper case letter, lower case letter, number and a special character")]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password do not match")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".jpg" }, ErrorMessage = "Only JPG files are allowed.")]
        public IFormFile Photo { get; set; }


    }
}

