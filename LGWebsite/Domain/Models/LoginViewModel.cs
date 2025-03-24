using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? PassWord { get; set; }


        [Required]
        public UserRole Role { get; set; }

    }
    public enum UserRole
    {
        Admin = 0,
        User = 1,
        Client = 2
    }
}
