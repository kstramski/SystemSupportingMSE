using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SystemSupportingMSE.Controllers.Resource;
using SystemSupportingMSE.Core;
using SystemSupportingMSE.Core.Models;
using SystemSupportingMSE.Extensions;
using SystemSupportingMSE.Helpers;

namespace SystemSupportingMSE.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly SportEventsDbContext context;

        public UserRepository(SportEventsDbContext context)
        {
            this.context = context;
        }

        public async Task<QueryResult<User>> GetUsers(UserQuery queryObj, bool filter)
        {
            var result = new QueryResult<User>();
            var query = context.Users
                .Include(g => g.Gender)
                .Include(r => r.Roles)
                    .ThenInclude(ur => ur.Role)
                .AsQueryable();

            if (queryObj.RoleId.HasValue)
                query.Where(q => q.Roles.Any(r => r.RoleId == queryObj.RoleId));

            var columnMap = new Dictionary<string, Expression<Func<User, object>>>
            {
                ["name"] = u => u.Name,
                ["surname"] = u => u.Surname,
                ["email"] = u => u.Email,
            };

            query = query.ApplyOrderBy(queryObj, columnMap);
            result.TotalItems = query.Count();

            if (filter == false)
                query = query.ApplyPaging(queryObj);
            result.Items = await query.ToListAsync();

            return result;
        }

        public async Task<User> GetUser(int id, string email)
        {
            var user = context.Users.AsQueryable();

            if (id == 0 && !String.IsNullOrWhiteSpace(email))
                return await user.SingleOrDefaultAsync(u => u.Email == email);

            return await user
                .Include(g => g.Gender)
                .Include(t => t.Teams)
                    .ThenInclude(ut => ut.Team)
                .Include(r => r.Roles)
                    .ThenInclude(ur => ur.Role)
                .SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> AuthenticateUser(string email, string password)
        {
            var user = await context.Users
                .Include(r => r.Roles)
                    .ThenInclude(ur => ur.Role)
                .SingleOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return user;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return user = null;

            return user;
        }

        public void Add(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePaswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            context.Users.Add(user);
        }

        public void Remove(User user)
        {
            context.Remove(user);
        }

        public void SetNewPassword(User user, string password, string newPassword)
        {
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new ArgumentException("Invalid password");

            byte[] passwordHash, passwordSalt;
            CreatePaswordHash(newPassword, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
        }

        private static void CreatePaswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < passwordHash.Length; i++)
                {
                    if (passwordHash[i] != storedHash[i])
                        return false;
                }
            }

            return true;
        }

        public Dashboard ChartsData(IEnumerable<User> users)
        {
            var data = new Dashboard();
            var today = DateTime.Now;
            var usersAge = GetUsersAge(users, today);

            data.Genders.Add(new KeyValuePairChart { Name = "Male", Items = CountGenders(users, "Male") });
            data.Genders.Add(new KeyValuePairChart { Name = "Female", Items = CountGenders(users, "Female") });

            data.Emails.Add(new KeyValuePairChart { Name = "Confirmed", Items = CountEmails(users, true) });
            data.Emails.Add(new KeyValuePairChart { Name = "Not Confirmed", Items = CountEmails(users, false) });

            data.UsersAge.Add(new KeyValuePairChart { Name = "<=15", Items = CountUsersAge(usersAge, 0, 15) });
            data.UsersAge.Add(new KeyValuePairChart { Name = "16-20", Items = CountUsersAge(usersAge, 16, 20) });
            data.UsersAge.Add(new KeyValuePairChart { Name = "21-25", Items = CountUsersAge(usersAge, 21, 25) });
            data.UsersAge.Add(new KeyValuePairChart { Name = "26-30", Items = CountUsersAge(usersAge, 26, 30) });
            data.UsersAge.Add(new KeyValuePairChart { Name = "31-40", Items = CountUsersAge(usersAge, 31, 40) });
            data.UsersAge.Add(new KeyValuePairChart { Name = "41-50", Items = CountUsersAge(usersAge, 41, 50) });
            data.UsersAge.Add(new KeyValuePairChart { Name = ">50", Items = CountUsersAge(usersAge, 51, 100) });

            data.UsersRegistered.Add(new KeyValuePairChart { Name = today.AddDays(-6).ToString("dd-MM"), Items = CountRegistered(users, today.AddDays(-6).Date) });
            data.UsersRegistered.Add(new KeyValuePairChart { Name = today.AddDays(-5).ToString("dd-MM"), Items = CountRegistered(users, today.AddDays(-5).Date) });
            data.UsersRegistered.Add(new KeyValuePairChart { Name = today.AddDays(-4).ToString("dd-MM"), Items = CountRegistered(users, today.AddDays(-4).Date) });
            data.UsersRegistered.Add(new KeyValuePairChart { Name = today.AddDays(-3).ToString("dd-MM"), Items = CountRegistered(users, today.AddDays(-3).Date) });
            data.UsersRegistered.Add(new KeyValuePairChart { Name = today.AddDays(-2).ToString("dd-MM"), Items = CountRegistered(users, today.AddDays(-2).Date) });
            data.UsersRegistered.Add(new KeyValuePairChart { Name = today.AddDays(-1).ToString("dd-MM"), Items = CountRegistered(users, today.AddDays(-1).Date) });
            data.UsersRegistered.Add(new KeyValuePairChart { Name = today.ToString("dd-MM"), Items = CountRegistered(users, today.Date) });

            return data;
        }

        private int CountGenders(IEnumerable<User> users, string name)
        {
            return users.Count(u => u.Gender.Name == name);
        }

        private int CountRegistered(IEnumerable<User> users, DateTime date)
        {
            return users.Count(u => u.DateOfRegistration.Date == date);
        }

        private int CountEmails(IEnumerable<User> users, bool confirmed)
        {
            return users.Count(u => u.EmailConfirmed == confirmed);
        }

        private IEnumerable<int> GetUsersAge(IEnumerable<User> users, DateTime today)
        {
            var usersAge = new List<int>();
            foreach (var user in users)
            {
                var age = today.Year - user.BirthDate.Year;
                if (today.AddYears(-age) > user.BirthDate)
                    age--;
                usersAge.Add(age);
            }
            return usersAge;
        }

        private int CountUsersAge(IEnumerable<int> usersAge, int age1, int age2)
        {
            return usersAge.Count(a => a >= age1 && a <= age2);
        }


    }
}