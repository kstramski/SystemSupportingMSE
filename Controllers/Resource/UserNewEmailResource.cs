using System.ComponentModel.DataAnnotations;

namespace SystemSupportingMSE.Controllers.Resource
{
    public class UserNewEmailResource
    {
         public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string NewEmail { get; set; }
    }
}