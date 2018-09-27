
using System.Security.Claims;

namespace SystemSupportingMSE.Core
{
    public interface IAuthRepository
    {
        bool IsModerator(ClaimsPrincipal user);
        bool IsAuthorizedById(ClaimsPrincipal user, int id);
    }
}