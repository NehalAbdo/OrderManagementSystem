using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.DTO
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set;}
        [Required]
        public string UserName { get; set;}
        [Required]
        [Phone]
        public string PhoneNumber { get; set;}
        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, and one digit.")]
        public string Password { get; set;}
        public string Role { get; set; } 


    }
}
