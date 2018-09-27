using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using SystemSupportingMSE.Core.Models;

namespace SystemSupportingMSE.Controllers.Resource.Teams
{
    public class TeamResource
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int Captain { get; set; }

        public ICollection<TeamUserDataResource> Users { get; set; }

        public TeamResource()
        {
            this.Users = new Collection<TeamUserDataResource>();
        }
    }
}