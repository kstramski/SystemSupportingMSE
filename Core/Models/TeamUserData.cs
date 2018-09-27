using System.ComponentModel.DataAnnotations;

namespace SystemSupportingMSE.Core.Models
{
    public class TeamUserData
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }
        
        public bool Status { get; set; }
    }
}