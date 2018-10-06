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
    }
}