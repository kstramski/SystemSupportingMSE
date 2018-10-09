using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemSupportingMSE.Core.Models.Events
{
    [Table("Groups")]
    public class Group
    {
        public byte Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public DateTime GroupStarts { get; set; }

    }
}