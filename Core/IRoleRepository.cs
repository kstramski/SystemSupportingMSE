using System.Collections.Generic;
using System.Threading.Tasks;
using SystemSupportingMSE.Controllers.Resource.Roles;
using SystemSupportingMSE.Core.Models;

namespace SystemSupportingMSE.Core
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetRoles();
        Task<Role> GetRole(int id);
        Task<int> RoleCount(int id);
        Task<bool> CheckAdmin(UserSaveRolesResource userSaveRolesResource, User user);
    }
}