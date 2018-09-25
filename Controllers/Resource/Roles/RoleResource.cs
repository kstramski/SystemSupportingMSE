using System.ComponentModel.DataAnnotations;

namespace SystemSupportingMSE.Controllers.Resource.Roles
{
    public class RoleResource
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }
    }
}