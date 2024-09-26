using Microsoft.AspNetCore.Http;
using MyPlanner.Shared.Services.Interfaces;

namespace MyPlanner.Shared.Services.Impl
{
    public class IdentityService(IHttpContextAccessor context) : IIdentityService
    {
        public string GetUserIdentity()
            => context.HttpContext?.User.FindFirst("sub")?.Value;

        public string GetUserName()
            => context.HttpContext?.User.Identity?.Name;
    }

}
