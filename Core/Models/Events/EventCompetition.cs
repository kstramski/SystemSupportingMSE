using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public DateTime RegistrationStarts { get; set; }
        public DateTime RegistrationEnds { get; set; }

        public DateTime CompetitionDate { get; set; }
        public TimeSpan? TimePerGroup { get; set; }

        public ICollection<UserCompetition> UsersCompetitions { get; set; }

        public EventCompetition()
        {
            UsersCompetitions = new Collection<UserCompetition>();
        }
    }
}