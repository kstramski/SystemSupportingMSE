using System.Collections.Generic;
using System.Threading.Tasks;
using SystemSupportingMSE.Core.Models.Events;

namespace SystemSupportingMSE.Core
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetEvents();
        Task<Event> GetEvent(int id);
        void Add(Event e);
        void Remove(Event e);
        bool CompetitionExist(Event e, int competitionId);
        Task<EventCompetition> GetEventCompetition(int eventId, int competitionId);
        Task<UserCompetition> FindUserCompetition(int userId, int eventId, int competitionId);
        void AddUserToCompetition(int userId, int eventId, int competitionId);
        void RemoveUserFromEvent(UserCompetition userCompetition);
    }
}