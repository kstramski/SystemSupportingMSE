using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SystemSupportingMSE.Core;
using SystemSupportingMSE.Core.Models;
using SystemSupportingMSE.Core.Models.Query;
using SystemSupportingMSE.Extensions;
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

        public async Task<QueryResult<Team>> GetTeams(TeamQuery queryObj)
        {
            var result = new QueryResult<Team>();
            var query = context.Teams
                .Include(t => t.Users)
                    .ThenInclude(ut => ut.User)
                .AsQueryable();

            var columnMap = new Dictionary<string, Expression<Func<Team, object>>>
            {
                ["name"] = t => t.Name,
            };

            query = query.ApplyOrderBy(queryObj, columnMap);
            result.TotalItems = query.Count();

            query = query.ApplyPaging(queryObj);
            result.Items = await query.ToListAsync();

            return result;
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