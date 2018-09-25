using System.ComponentModel.DataAnnotations;

namespace SystemSupportingMSE.Controllers.Resource.Users
{
    public class UserNewPasswordResource
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [StringLength(255)]
        public string NewPassword { get; set; }

        [Required]
        [StringLength(255)]
        public string NewPasswordRepeat { get; set; }
    }
}