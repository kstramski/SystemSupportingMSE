using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace SystemSupportingMSE.Controllers.Resource.Roles
{
    public class UserSaveRolesResource
    {
        public int Id { get; set; }

        public ICollection<int> Roles { get; set; }

        public UserSaveRolesResource()
        {
            this.Roles = new Collection<int>();
        }
    }
}