using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemSupportingMSE.Core.Models.Events
{
    [Table("EventsCompetitions")]
    public class EventCompetition
    {
        public int EventId { get; set; }
        public int CompetitionId { get; set; }
        public Event Event { get; set; }
        public Competition Competition { get; set; }

        // public DateTime CompetitionDate { get; set; }
        // public TimeSpan CompetitionTime { get; set; }
    }
}