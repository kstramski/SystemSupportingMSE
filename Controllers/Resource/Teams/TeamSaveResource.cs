using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace SystemSupportingMSE.Controllers.Resource.Teams
{
    public class TeamSaveResource
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int Captain { get; set; }
    }
}