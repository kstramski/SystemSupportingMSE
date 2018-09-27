using System.Collections.Generic;
using System.Threading.Tasks;
using SystemSupportingMSE.Core.Models;

namespace SystemSupportingMSE.Core
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetTeams();
        Task<Team> GetTeam(int id);
        void Add(Team team);
        void Remove(Team team);
        UserTeam GetUserTeam(Team team, int id);
    }
}