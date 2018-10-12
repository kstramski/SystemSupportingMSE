using System.Collections.Generic;
using System.Collections.ObjectModel;
using SystemSupportingMSE.Controllers.Resource.Users;

namespace SystemSupportingMSE.Controllers.Resource.UsersCompetitions
{
    public class CompetitionUsersResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public int UsersCount { get; set; }
    }
}