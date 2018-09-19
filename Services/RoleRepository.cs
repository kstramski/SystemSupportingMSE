using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
    }
}