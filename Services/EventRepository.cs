using System;
using System.Collections.Generic;
using System.Linq;
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
                .Include(e => e.Competitions)
                    .ThenInclude(ec => ec.UsersCompetitions)
                        .ThenInclude(uc => uc.User)
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

        public bool CompetitionExist(Event e, int competitionId)
        {
            return e.Competitions.Any(ec => ec.CompetitionId == competitionId);
        }

        public void AddDatesToCompetitions(Event e)
        {
            foreach (var competition in e.Competitions)
            {
                var def = new DateTime(1, 1, 1, 0, 0, 0);

                if (competition.CompetitionDate <= def)
                    competition.CompetitionDate = e.EventStarts;

                if (competition.RegistrationStarts <= def)
                    competition.RegistrationStarts = DateTime.Now;

                if (competition.RegistrationEnds <= def)
                    competition.RegistrationEnds = e.EventStarts;

                if (competition.Competition.GroupsRequired)
                    competition.TimePerGroup = new TimeSpan(0, 15, 0);
            }
        }

        public Task<EventCompetition> GetEventCompetition(int eventId, int competitionId)
        {
            return context.EventsCompetitions
                .Include(ec => ec.Event)
                .Include(ec => ec.Competition)
                .Include(ec => ec.UsersCompetitions)
                    .ThenInclude(uc => uc.User)
                .SingleOrDefaultAsync(ec => ec.EventId == eventId && ec.CompetitionId == competitionId);
        }

        public async Task<UserCompetition> FindUserCompetition(int userId, int eventId, int competitionId)
        {
            return await context.UsersCompetitions.SingleOrDefaultAsync(uc => uc.CompetitionId == competitionId && uc.EventId == eventId && uc.UserId == userId);
        }

        public void AddUserToCompetition(int userId, int eventId, int competitionId)
        {
            context.UsersCompetitions.Add(new UserCompetition { EventId = eventId, CompetitionId = competitionId, UserId = userId });
        }

        public void RemoveUserFromEvent(UserCompetition userCompetition)
        {
            context.Remove(userCompetition);
        }
    }
}