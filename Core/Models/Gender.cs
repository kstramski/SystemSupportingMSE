using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemSupportingMSE.Core.Models
{
    [Table("Genders")]
    public class Gender
    {
        public byte Id { get; set; }

        [Required]
        [StringLength(6)]
        public string Name { get; set; }
    }
}