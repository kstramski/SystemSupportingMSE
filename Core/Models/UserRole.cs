using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemSupportingMSE.Core.Models
{
    [Table("UserRoles")]
    public class UserRole
    {
        public int UserId { get; set; }

        public int RoleId { get; set; }

        public User User { get; set; }

        public Role Role { get; set; }
    }
}