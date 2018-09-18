using System.Collections.Generic;
using System.Threading.Tasks;
using SystemSupportingMSE.Core.Models;

namespace SystemSupportingMSE.Core
{
    public interface IUserRepository
    {
        Task<User> GetUser(int id);
        Task<IEnumerable<User>> GetUsers();
        void Add(User user, string password);
        void Remove(User user);
        void SetNewPassword(User user, string password, string newPassword);
        
    }
}