using System.Collections.Generic;
using System.Threading.Tasks;
using SystemSupportingMSE.Core.Models;
using SystemSupportingMSE.Core.Models.Query;

namespace SystemSupportingMSE.Core
{
    public interface ITeamRepository
    {
        Task<QueryResult<Team>> GetTeams(TeamQuery queryObj);
        Task<Team> GetTeam(int id);
        void Add(Team team);
        void Remove(Team team);
        UserTeam GetUserTeam(Team team, int id);
    }
}