using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace SystemSupportingMSE.Controllers.Resource.Events
{
    public class EventSaveResource
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime EventStarts { get; set; }

        public DateTime EventEnds { get; set; }

        public ICollection<int> Competitions { get; set; }

        public EventSaveResource()
        {
            Competitions = new Collection<int>();
        }
    }
}