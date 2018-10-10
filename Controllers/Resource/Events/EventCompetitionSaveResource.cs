using System;

namespace SystemSupportingMSE.Controllers.Resource.Events
{
    public class EventCompetitionSaveResource
    {
        public int EventId { get; set; }
        public int CompetitionId { get; set; }
        
        public DateTime RegistrationStarts { get; set; }
        public DateTime RegistrationEnds { get; set; }

        public DateTime CompetitionDate { get; set; }
        public TimeSpan? TimePerGroup { get; set; }
    }
}