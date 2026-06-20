using Helm.Core.Application.Interfaces;

namespace Helm.Api
{
    public class UserContext(IHttpContextAccessor httpContextAccessor)
    : IUserContext
    {
        public string? Login => 
            httpContextAccessor
            .HttpContext?
            .User
            .Claims.FirstOrDefault(c=>c.Type.Contains("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value;
    }
}
