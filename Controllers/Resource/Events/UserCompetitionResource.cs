using SystemSupportingMSE.Controllers.Resource.Users;

namespace SystemSupportingMSE.Controllers.Resource.Events
{
    public class UserCompetitionResource
    {
        public int EventId { get; set; }
        public int CompetitionId { get; set; }
        public UserBasicsResource User { get; set; }
        public KeyValuePairResource Stage { get; set; }
        public byte? GroupId { get; set; }
    }
}