using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SystemSupportingMSE.Controllers.Resource.Roles;
using SystemSupportingMSE.Core;
using SystemSupportingMSE.Core.Models;
using SystemSupportingMSE.Helpers;

namespace SystemSupportingMSE.Services
{
    public class RoleRepository : IRoleRepository
    {
        private readonly SportEventsDbContext context;

        public RoleRepository(SportEventsDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await context.Roles.ToListAsync();
        }

        public async Task<Role> GetRole(int id)
        {
            return await context.Roles.SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<int> RoleCount(int id) 
        {
            var users =  await context.Users.Where(u => u.Roles.Any(ur => ur.RoleId == 1)).ToListAsync();

            return users.Count();
        }

        public async Task<bool> CheckAdmin(UserSaveRolesResource userSaveRolesResource, User user) 
        {   
            if(!userSaveRolesResource.Roles.Contains(1) && user.Roles.Any(r => r.RoleId == 1))
            {
                var count = await RoleCount(1);
                if(count <= 1)
                    return true;
            }
            return false;
        }
    }
}