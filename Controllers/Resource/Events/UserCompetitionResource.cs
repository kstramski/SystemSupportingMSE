using SystemSupportingMSE.Controllers.Resource.Users;

namespace SystemSupportingMSE.Controllers.Resource.Events
{
    public class UserCompetitionResource
    {
        public UserBasicsResource User { get; set; }
        public byte? StageId { get; set; }
        public byte? GroupId { get; set; }
    }
}