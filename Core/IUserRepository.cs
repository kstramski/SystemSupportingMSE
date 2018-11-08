using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SystemSupportingMSE.Core.Models;
using SystemSupportingMSE.Core.Models.Query;

namespace SystemSupportingMSE.Core
{
    public interface IUserRepository
    {
        Task<QueryResult<User>> GetUsers(UserQuery queryObj, bool filter = true);
        Task<User> GetUserById(int id);
        Task<User> GetUserByEmail(string email, bool full = true);
        Task<User> AuthenticateUser(string email, string password);
        void Add(User user, string password);
        void Remove(User user);
        void SetNewPassword(User user, string password, string newPassword);
        Task AddToken(int id, string email, string type);
        Task ConfirmToken(User user, string type);
        Task<UserToken> GetToken(string token);
        Dashboard ChartsData(IEnumerable<User> users);

    }
}