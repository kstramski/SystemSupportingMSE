using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemSupportingMSE.Core.Models.Events
{
    [Table("Events")]
    public class Event
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime EventStarts { get; set; }

        public DateTime EventEnds { get; set; }

        // public DateTime RegistrationStarts { get; set; }

        // public DateTime RegistrationEnds { get; set; }

        public ICollection<EventCompetition> Competitions { get; set; }

        public Event()
        {
            Competitions = new Collection<EventCompetition>();
        }
    }
}