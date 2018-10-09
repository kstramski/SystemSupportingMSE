using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SystemSupportingMSE.Controllers.Resource.UsersCompetitions;

namespace SystemSupportingMSE.Controllers.Resource.Events
{
    public class EventUsersResource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime EventStarts { get; set; }

        public DateTime EventEnds { get; set; }

        public ICollection<CompetitionUsersResource> Competitions { get; set; }

        public EventUsersResource()
        {
            Competitions = new Collection<CompetitionUsersResource>();
        }
    }
}