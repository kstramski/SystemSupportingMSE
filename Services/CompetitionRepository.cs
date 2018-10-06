using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SystemSupportingMSE.Core;
using SystemSupportingMSE.Core.Models.Events;
using SystemSupportingMSE.Helpers;

namespace SystemSupportingMSE.Services
{
    public class CompetitionRepository : ICompetitionRepository
    {
        private readonly SportEventsDbContext context;

        public CompetitionRepository(SportEventsDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Competition>> GetCompetitions()
        {
            return await context.Competitions
                .Include(c => c.Events)
                    .ThenInclude(ec => ec.Event)
                .ToListAsync();
        }
        public async Task<Competition> GetCompetition(int id)
        {
            return await context.Competitions
                .Include(c => c.Events)
                    .ThenInclude(ec => ec.Event)
                .SingleOrDefaultAsync(c => c.Id == id);
        }
        public void Add(Competition competition)
        {
            context.Competitions.Add(competition);
        }
        public void Remove(Competition competition)
        {
            context.Remove(competition);
        }
    }
}