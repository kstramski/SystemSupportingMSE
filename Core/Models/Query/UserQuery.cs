using SystemSupportingMSE.Extensions;

namespace SystemSupportingMSE.Core.Models.Query
{
    public class UserQuery : IQueryObject
    {
        public int? EventId { get; set; }
        public int? TeamId { get; set; }
        public int? RoleId { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}