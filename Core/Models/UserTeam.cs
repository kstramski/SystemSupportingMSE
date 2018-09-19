using System.ComponentModel.DataAnnotations.Schema;

namespace SystemSupportingMSE.Core.Models
{
    [Table("UserTeams")]
    public class UserTeam
    {
        public int UserId { get; set; }
        public int TeamId { get; set; }

        public User User { get; set; }
        public Team Team { get; set; }
    }
}