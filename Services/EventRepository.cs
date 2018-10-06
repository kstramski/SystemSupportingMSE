using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SystemSupportingMSE.Core;
using SystemSupportingMSE.Core.Models.Events;
using SystemSupportingMSE.Helpers;

namespace SystemSupportingMSE.Services
{
    public class EventRepository : IEventRepository
    {
        private readonly SportEventsDbContext context;

        public EventRepository(SportEventsDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Event>> GetEvents()
        {
            return await context.Events
                .Include(e => e.Competitions)
                    .ThenInclude(ec => ec.Competition)
                .ToListAsync();
        }

        public async Task<Event> GetEvent(int id)
        {
            return await context.Events
                .Include(e => e.Competitions)
                    .ThenInclude(ec => ec.Competition)
                .SingleOrDefaultAsync(e => e.Id == id);
        }

        public void Add(Event e)
        {
            context.Events.Add(e);
        }

        public void Remove(Event e)
        {
            context.Remove(e);
        }
    }
}