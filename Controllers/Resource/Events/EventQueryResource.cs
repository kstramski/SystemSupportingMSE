using SystemSupportingMSE.Extensions;

namespace SystemSupportingMSE.Controllers.Resource.Events
{
    public class EventQueryResource : IQueryObject
    {
        public int? CompetitionId { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}