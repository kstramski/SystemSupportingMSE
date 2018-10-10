using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace SystemSupportingMSE.Controllers.Resource.Events
{
    public class EventResource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime EventStarts { get; set; }

        public DateTime EventEnds { get; set; }

        public bool IsActive { get; set; }

        public ICollection<KeyValuePairResource> Competitions { get; set; }

        public EventResource()
        {
            Competitions = new Collection<KeyValuePairResource>();
        }
    }
}