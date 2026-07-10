using Helm.Application.Interfaces;
using System.Security.Claims;

namespace Helm.Api
{
    public class UserContext(IHttpContextAccessor httpContextAccessor)
    : IUserContext
    {
        public string? Login => 
            httpContextAccessor
            .HttpContext?
            .User
            .Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}
