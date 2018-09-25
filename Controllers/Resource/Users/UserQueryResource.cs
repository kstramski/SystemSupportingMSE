using SystemSupportingMSE.Extensions;

namespace SystemSupportingMSE.Controllers.Resource.Users
{
    public class UserQueryResource : IQueryObject
    {
        public int? RoleId { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}