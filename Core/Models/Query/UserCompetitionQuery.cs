using SystemSupportingMSE.Extensions;

namespace SystemSupportingMSE.Core.Models.Query
{
    public class UserCompetitionQuery : IQueryObject
    {
        public int? StageId { get; set; }
        public int? GroupId { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public bool Paging { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}