using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using SystemSupportingMSE.Core;

namespace SystemSupportingMSE.Services
{
    public class AuthRepository : IAuthRepository
    {
        public bool IsModerator(ClaimsPrincipal user)
        {
            return (user.Claims.Where(c => c.Type == ClaimTypes.Role).Where(r => r.Value == "Moderator").Any()) ? true : false;
        }

        public bool IsAuthorizedById(ClaimsPrincipal user, int id)
        {
            return (user.FindFirst(ClaimTypes.NameIdentifier).Value == id.ToString()) ? true : false;
        }
    }
}