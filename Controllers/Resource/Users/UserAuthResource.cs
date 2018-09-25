using System.ComponentModel.DataAnnotations;

namespace SystemSupportingMSE.Controllers.Resource.Users
{
//Na później

    public class UserAuthResource
    {
        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

    }
}