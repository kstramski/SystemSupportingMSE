using System.ComponentModel.DataAnnotations;

namespace SystemSupportingMSE.Controllers.Resource.Teams
{
    public class TeamSaveUserResource
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }
    }
}