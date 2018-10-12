using System.Collections.Generic;
using System.Threading.Tasks;
using SystemSupportingMSE.Core.Models;
using SystemSupportingMSE.Core.Models.Events;
using SystemSupportingMSE.Core.Models.Query;

namespace SystemSupportingMSE.Core
{
    public interface IEventRepository
    {
        Task<QueryResult<Event>> GetEvents(EventQuery queryObj);
        Task<Event> GetEvent(int id);
        void Add(Event e);
        void Remove(Event e);
        bool CompetitionExist(Event e, int competitionId);
        void AddDatesToCompetitions(Event e);
        Task<EventCompetition> GetEventCompetition(int eventId, int competitionId);
        Task<IEnumerable<UserCompetition>> GetEventCompetitionUsers(int eventId, int competitionId);
        Task<UserCompetition> FindUserCompetition(int userId, int eventId, int competitionId);
        void AddUserToCompetition(int userId, int eventId, int competitionId);
        void RemoveUserFromEvent(UserCompetition userCompetition);
    }
}