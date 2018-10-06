using System.Collections.Generic;
using System.Threading.Tasks;
using SystemSupportingMSE.Core.Models.Events;

namespace SystemSupportingMSE.Core
{
    public interface ICompetitionRepository
    {
        Task<IEnumerable<Competition>> GetCompetitions();
        Task<Competition> GetCompetition(int id);
        void Add(Competition competition);
        void Remove(Competition competition);
    }
}