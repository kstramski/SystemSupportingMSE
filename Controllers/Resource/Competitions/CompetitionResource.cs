using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace SystemSupportingMSE.Controllers.Resource.Competitions
{
    public class CompetitionResource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool GroupsRequired { get; set; }

        public int? GroupSize { get; set; }

        public ICollection<KeyValuePairResource> Events { get; set; }

        public CompetitionResource()
        {
            Events = new Collection<KeyValuePairResource>();
        }
    }
}