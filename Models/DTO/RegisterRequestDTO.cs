using System.ComponentModel.DataAnnotations;

namespace NZworks.Models.DTO
{
    public class RegisterRequestDTO
    {
        [Required(ErrorMessage = "Username is required")]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string[] Roles { get; set; }  // Optional: Roles for the user

    }
}
