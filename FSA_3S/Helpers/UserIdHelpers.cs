using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace FSA_3S.Helpers
{
    public static class UserIdHelper
    {
        public static int? GetUserId(IHttpContextAccessor httpContextAccessor)
        {
            var userIdClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out int userId))
            {
                return userId;
            }
            return null;
        }
    }
}