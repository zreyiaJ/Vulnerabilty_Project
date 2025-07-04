using System.ComponentModel.DataAnnotations;

namespace Vulnerabilty_Project.Models
{

    public class UserModel : RoleModel
    {
        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be at least 1 character.")]
        public string Name { get; set; }



        [StringLength(10, MinimumLength =9, ErrorMessage = "Enter number length 10")]
        public string PhoneNo { get; set; }
    }

}