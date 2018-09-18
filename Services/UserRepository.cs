using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SystemSupportingMSE.Controllers.Resource;
using SystemSupportingMSE.Core;
using SystemSupportingMSE.Core.Models;
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

        public void Add(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePaswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            context.Users.Add(user);
        }



        public async Task<User> GetUser(int id)
        {
            return await context.Users
                .Include(t => t.Team)
                .SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await context.Users.ToListAsync();
        }

        public void Remove(User user)
        {
            context.Remove(user);
        }

        public void SetNewPassword(User user, string password, string newPassword)
        {
            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new ArgumentException("Invalid password");

            byte[] passwordHash, passwordSalt;
            CreatePaswordHash(newPassword, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
        }

        private static void CreatePaswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using(var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < passwordHash.Length; i++)
                {
                    if(passwordHash[i] != storedHash[i])
                        return false;
                }
            }

            return true;
        }
    }
}