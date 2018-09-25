using System.Collections.Generic;
using System.Threading.Tasks;
using SystemSupportingMSE.Core.Models;

namespace SystemSupportingMSE.Core
{
    public interface IUserRepository
    {
        Task<QueryResult<User>> GetUsers(UserQuery queryObj);
        Task<User> GetUser(int id);
        Task<User> AuthenticateUser(string email, string password);
        void Add(User user, string password);
        void Remove(User user);
        void SetNewPassword(User user, string password, string newPassword);
        
    }
}