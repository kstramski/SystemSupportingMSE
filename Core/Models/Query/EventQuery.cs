using SystemSupportingMSE.Extensions;

namespace SystemSupportingMSE.Core.Models.Query
{
    public class EventQuery : IQueryObject
    {
        public int? CompetitionId { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}