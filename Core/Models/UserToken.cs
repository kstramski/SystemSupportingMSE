using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemSupportingMSE.Core.Models
{
    [Table("UserTokens")]
    public class UserToken
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        [StringLength(255)]
        public string Token { get; set; }

        public DateTime ExpirationDate { get; set; }

        public bool IsActive { get; set; }
    }
}