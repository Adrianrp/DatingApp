using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    // This is a Data Transfer Object, they are commonly used in MVC
    // to map model objects into simple objects that replaced or displayed ny the view
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }    

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify a password between 4 and 8 characters")]
        public string Password { get; set; }
    }
}