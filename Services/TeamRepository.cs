using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SystemSupportingMSE.Core;
using SystemSupportingMSE.Core.Models;
using SystemSupportingMSE.Helpers;

namespace SystemSupportingMSE.Services
{
    public class TeamRepository : ITeamRepository
    {
        private readonly SportEventsDbContext context;

        public TeamRepository(SportEventsDbContext context)
        {
            this.context = context;
        }

        public async Task<Team> GetTeam(int id)
        {
            return await context.Teams
                .Include(t => t.Users)
                    .ThenInclude(ut => ut.User)
                .Where(t => t.Id == id)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await context.Teams
                .Include(t => t.Users)
                    .ThenInclude(ut => ut.User)
                .ToListAsync();
        }

        public void Add(Team team)
        {
            context.Teams.Add(team);
        }

        public void Remove(Team team)
        {
            context.Remove(team);
        }

        public UserTeam GetUserTeam(Team team, int id)
        {
            return team.Users.SingleOrDefault(ut => ut.UserId == id);
        }

    }
}