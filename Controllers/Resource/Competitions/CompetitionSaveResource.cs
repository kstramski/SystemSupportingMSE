using System.ComponentModel.DataAnnotations;

namespace SystemSupportingMSE.Controllers.Resource.Competitions
{
    public class CompetitionSaveResource
    {
         public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public bool GroupsRequired { get; set; }

        public int? GroupSize { get; set; }

    }
}