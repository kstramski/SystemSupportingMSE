using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace SystemSupportingMSE.Controllers.Resource.Roles
{
    public class UserRolesResource
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        public ICollection<KeyValuePairResource> Roles { get; set; }

        public UserRolesResource()
        {
            this.Roles = new Collection<KeyValuePairResource>();
        }
    }
}