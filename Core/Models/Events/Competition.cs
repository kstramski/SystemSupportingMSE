using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemSupportingMSE.Core.Models.Events
{
    [Table("Competitions")]
    public class Competition
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public bool GroupsRequired { get; set; }

        public int? GroupSize { get; set; } //byte

        // public bool TeamRequired { get; set; }

        // public byte? TeamSize { get; set; }

        public ICollection<EventCompetition> Events { get; set; }

        public Competition()
        {
            Events = new Collection<EventCompetition>();
        }
    }
}