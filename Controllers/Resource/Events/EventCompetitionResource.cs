using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SystemSupportingMSE.Controllers.Resource.Events
{
    public class EventCompetitionResource
    {
        public KeyValuePairResource Event { get; set; }
        public KeyValuePairResource Competition { get; set; }

        public DateTime RegistrationStarts { get; set; }
        public DateTime RegistrationEnds { get; set; }
        
        public DateTime CompetitionDate { get; set; }
        public bool GroupRequired { get; set; }
        public TimeSpan? TimePerGroup { get; set; }

        public int UsersCount { get; set; }
    }
}