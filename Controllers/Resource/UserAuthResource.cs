using System.ComponentModel.DataAnnotations;

namespace SystemSupportingMSE.Controllers.Resource
{
//Na później

    public class UserAuthResource
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

    }
}