using System.ComponentModel.DataAnnotations.Schema;

namespace SystemSupportingMSE.Core.Models.Events
{
    [Table("UsersCompetitions")]
    public class UserCompetition
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int EventId { get; set; }
        public int CompetitionId { get; set; }
        public EventCompetition EventCompetition { get; set; }

        public byte StageId { get; set; }
        public Stage Stage { get; set; }

        public byte? GroupId { get; set; }
        // public Group Group { get; set; }
    }
}